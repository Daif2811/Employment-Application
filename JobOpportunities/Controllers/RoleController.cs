using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobOpportunities.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
namespace JobOpportunities.Controllers
{

    public class RoleController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Role
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }




        // GET: Role/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }






        // GET: Role/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);


        }






        // GET: Role/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name")]IdentityRole role)
        {

            if (ModelState.IsValid)
            {
                // We use this code in Editting
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(role);
        }





        // GET: Role/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }

            return View(role);
        }


        // POST: Role/Delete/5
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id,Name")] IdentityRole role)
        {
            // we put variable to find the role by    role.id   and delete the variable
            var del = db.Roles.Find(role.Id);
            db.Roles.Remove(del);
            db.SaveChanges();

            return RedirectToAction("Index");
        }




    }
}
