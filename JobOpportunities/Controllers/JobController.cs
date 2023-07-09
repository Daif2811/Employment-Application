using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobOpportunities.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace JobOpportunities.Controllers
{

    [Authorize]
    public class JobController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [AllowAnonymous]
        // GET: Job
        public ActionResult Index()
        {
            var jobs = db.Jobs.Include(j => j.Category);
            return View(jobs.ToList());
        }

       


        // GET: Job/Details/5
        public ActionResult Details(int? id)
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
            return View(job);
        }



        // GET: Job/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Job/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,JobTitle,JobContent,JobImage,CategoryID")] Job job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                //    1                  1                                    111                               222
                // Path class       combine => to bind 2 paths   ( server.mapPath ( path on project ) , upload.FileName == image name )
                string path = Path.Combine(Server.MapPath("~/Images/Jobs/"), upload.FileName);
                upload.SaveAs(path);              // then put path which saved the path on it    up
                job.JobImage = upload.FileName;   // jobImage from   object job     to save path on database
                job.UserId = User.Identity.GetUserId();

                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", job.CategoryID);
            return View(job);
        }




        // GET: Job/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", job.CategoryID);
            return View(job);
        }

        // POST: Job/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,JobTitle,JobContent,JobImage,CategoryID")] Job job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                // another variable  to store the last  path  and delete  the last image if you put another image by the next code
                string oldPath = Path.Combine(Server.MapPath("~/Images/Jobs"), job.JobImage);

                if (upload != null)
                {
                    //if you put another image            delete the last image from server
                    System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Images/Jobs"), upload.FileName);
                    upload.SaveAs(path);
                    job.JobImage = upload.FileName;
                   
                }
                 
                 
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", job.CategoryID);
            return View(job);
        }





        // GET: Job/Delete/5
        public ActionResult Delete(int? id)
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
            return View(job);
        }



        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
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
