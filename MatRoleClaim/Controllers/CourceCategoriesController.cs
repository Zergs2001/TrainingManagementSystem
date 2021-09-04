using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MatRoleClaim.Models;

namespace MatRoleClaim.Controllers
{
    public class CourceCategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourceCategories
        public ActionResult Index()
        {
            return View(db.CourceCategorys.ToList());
        }

        // GET: CourceCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourceCategory courceCategory = db.CourceCategorys.Find(id);
            if (courceCategory == null)
            {
                return HttpNotFound();
            }
            return View(courceCategory);
        }

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
        public ActionResult Create([Bind(Include = "Id,Name")] CourceCategory courceCategory)
        {
            if (ModelState.IsValid)
            {
                db.CourceCategorys.Add(courceCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courceCategory);
        }

        // GET: CourceCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourceCategory courceCategory = db.CourceCategorys.Find(id);
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
        public ActionResult Edit([Bind(Include = "Id,Name")] CourceCategory courceCategory)
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
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourceCategory courceCategory = db.CourceCategorys.Find(id);
            if (courceCategory == null)
            {
                return HttpNotFound();
            }
            return View(courceCategory);
        }

        // POST: CourceCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourceCategory courceCategory = db.CourceCategorys.Find(id);
            db.CourceCategorys.Remove(courceCategory);
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
