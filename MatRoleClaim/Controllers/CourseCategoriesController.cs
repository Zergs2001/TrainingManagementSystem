using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MatRoleClaim.Attributes;
using MatRoleClaim.Models;

namespace MatRoleClaim.Controllers
{
    public class CourseCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourceCategories
        [RoleClaimsAuthorize("CourseCategories", "Show")]
        public ActionResult Index()
        {
            return View(db.CourseCategorys.ToList());
        }

        // GET: CourceCategories/Details/5
        [RoleClaimsAuthorize("CourseCategories", "Show")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courceCategory = db.CourseCategorys.Find(id);
            if (courceCategory == null)
            {
                return HttpNotFound();
            }
            return View(courceCategory);
        }

        [RoleClaimsAuthorize("CourseCategories", "Add")]
        // GET: CourceCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourceCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleClaimsAuthorize("CourseCategories", "Add")]
        public ActionResult Create([Bind(Include = "Id,Name")] CourseCategory courceCategory)
        {
            if (ModelState.IsValid)
            {
                db.CourseCategorys.Add(courceCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courceCategory);
        }

        // GET: CourceCategories/Edit/5
        [RoleClaimsAuthorize("CourseCategories", "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courceCategory = db.CourseCategorys.Find(id);
            if (courceCategory == null)
            {
                return HttpNotFound();
            }
            return View(courceCategory);
        }

        // POST: CourceCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleClaimsAuthorize("CourseCategories", "Edit")]
        public ActionResult Edit([Bind(Include = "Id,Name")] CourseCategory courceCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courceCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courceCategory);
        }

        // GET: CourceCategories/Delete/5
        [RoleClaimsAuthorize("CourseCategories", "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courceCategory = db.CourseCategorys.Find(id);
            if (courceCategory == null)
            {
                return HttpNotFound();
            }
            return View(courceCategory);
        }

        // POST: CourceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [RoleClaimsAuthorize("CourseCategories", "Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseCategory courceCategory = db.CourseCategorys.Find(id);
            db.CourseCategorys.Remove(courceCategory);
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
