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
using MatRoleClaim.Models.IdentityModels;
using MatRoleClaim.Models.ViewModels;

namespace MatRoleClaim.Controllers
{
    public class CourcesController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Cources
        [RoleClaimsAuthorize("Cources", "Show")]
        public ActionResult Index()
        {
            var cources = db.Cources.Include(c => c.CourceCategory);
            return View(cources.ToList());
        }

        [RoleClaimsAuthorize("Cources", "Show")]
        // GET: Cources/Details/5
        public ActionResult Details(int? id)
        {
            ViewBag.Trainer = db.TrainerInCource.Where(x => x.CourceId == id);
            ViewBag.Trainee = db.TraineeInCource.Where(x => x.CourceId == id);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = db.Cources.Find(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            return View(cource);
        }

        // GET: Cources/Create
        [RoleClaimsAuthorize("Cources", "Add")]
        public ActionResult Create()
        {
            ViewBag.CourceCategoryId = new SelectList(db.CourceCategorys, "Id", "Name");
            List<ApplicationRole> allroles = DbContext.Roles.ToList();
            List<UserRolesViewModel> allusersWithRoles = new List<UserRolesViewModel>();
            List<UserRolesViewModel> allusersWithRoles1 = new List<UserRolesViewModel>();
            List<UserRolesViewModel> allusersWithRoles2 = new List<UserRolesViewModel>();
            foreach (var user in DbContext.Users)
            {
                UserRolesViewModel userWithRoles = new UserRolesViewModel { UserId = user.Id, UserName = user.UserName, UserEmail = user.Email, Roles = new List<RoleViewModel>() };
                user.Roles.ToList().ForEach(x => userWithRoles.Roles.Add((RoleViewModel)allroles.Find(y => y.Id == x.RoleId)));
                allusersWithRoles.Add(userWithRoles);
            }
            foreach (var item in allusersWithRoles)
            {
                foreach (var item1 in item.Roles)
                {
                    if (item1.Name == "Trainer")
                    {
                        allusersWithRoles1.Add(item);
                    }
                    else if(item1.Name == "Trainee")
                    {
                        allusersWithRoles2.Add(item);
                    }
                }
            }
            ViewBag.TrainerId = new SelectList(allusersWithRoles1, "UserId", "UserName");
            ViewBag.TraineeId = new SelectList(allusersWithRoles2, "UserId", "UserName");
            return View();
        }

        // POST: Cources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleClaimsAuthorize("Cources", "Add")]
        public ActionResult Create(Cource cource)
        {
            string TrainerId = Request.Form["TrainerId"];
            string TraineeId = Request.Form["TraineeId"];
            if (ModelState.IsValid)
            {
                db.Cources.Add(cource);
                db.SaveChanges();
                if (!string.IsNullOrEmpty(TrainerId))
                {
                    string[] arrListStr = TrainerId.Split(',');
                    foreach (var item in arrListStr)
                    {
                        var TrainerInCource = new TrainerInCource();
                        TrainerInCource.TrainerId = item;
                        TrainerInCource.CourceId = cource.Id;
                        db.TrainerInCource.Add(TrainerInCource);
                        db.SaveChanges();
                    }
                }
                if (!string.IsNullOrEmpty(TraineeId))
                {
                    string[] arrListStr1 = TraineeId.Split(',');
                    foreach (var item in arrListStr1)
                    {
                        var TraineeInCource = new TraineeInCource();
                        TraineeInCource.TraineeId = item;
                        TraineeInCource.CourceId = cource.Id;
                        db.TraineeInCource.Add(TraineeInCource);
                        db.SaveChanges();
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.CourceCategoryId = new SelectList(db.CourceCategorys, "Id", "Name", cource.CourceCategoryId);
            return View(cource);
        }
        [RoleClaimsAuthorize("Cources", "Add")]
        public ActionResult AddAccountInCource(int Id)
        {
            string TrainerId = Request.Form["TrainerId"];
            string TraineeId = Request.Form["TraineeId"];
            if (!string.IsNullOrEmpty(TrainerId))
            {
                string[] arrListStr = TrainerId.Split(',');
                foreach (var item in arrListStr)
                {
                    var output = db.TrainerInCource.Where(x => x.CourceId == Id && x.TrainerId == item).ToList();
                    if (output.Count > 0)
                    {
                        continue;
                    }
                    var TrainerInCource = new TrainerInCource();
                    TrainerInCource.TrainerId = item;
                    TrainerInCource.CourceId = Id;
                    db.TrainerInCource.Add(TrainerInCource);
                    db.SaveChanges();
                }
            }
            if (!string.IsNullOrEmpty(TraineeId))
            {
                string[] arrListStr1 = TraineeId.Split(',');
                foreach (var item in arrListStr1)
                {
                    var output = db.TraineeInCource.Where(x => x.CourceId == Id && x.TraineeId == item).ToList();
                    if (output.Count > 0)
                    {
                        continue;
                    }
                    var TraineeInCource = new TraineeInCource();
                    TraineeInCource.TraineeId = item;
                    TraineeInCource.CourceId = Id;
                    db.TraineeInCource.Add(TraineeInCource);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Edit", new { id = Id });
        }
        // GET: Cources/Edit/5
        [RoleClaimsAuthorize("Cources", "Edit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = db.Cources.Find(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            ViewBag.Trainer = db.TrainerInCource.Where(x => x.CourceId == cource.Id);
            ViewBag.Trainee = db.TraineeInCource.Where(x => x.CourceId == cource.Id);
            ViewBag.CourceCategoryId = new SelectList(db.CourceCategorys, "Id", "Name", cource.CourceCategoryId);
            List<ApplicationRole> allroles = DbContext.Roles.ToList();
            List<UserRolesViewModel> allusersWithRoles = new List<UserRolesViewModel>();
            List<UserRolesViewModel> allusersWithRoles1 = new List<UserRolesViewModel>();
            List<UserRolesViewModel> allusersWithRoles2 = new List<UserRolesViewModel>();
            foreach (var user in DbContext.Users)
            {
                UserRolesViewModel userWithRoles = new UserRolesViewModel { UserId = user.Id, UserName = user.UserName, UserEmail = user.Email, Roles = new List<RoleViewModel>() };
                user.Roles.ToList().ForEach(x => userWithRoles.Roles.Add((RoleViewModel)allroles.Find(y => y.Id == x.RoleId)));
                allusersWithRoles.Add(userWithRoles);
            }
            foreach (var item in allusersWithRoles)
            {
                foreach (var item1 in item.Roles)
                {
                    if (item1.Name == "Trainer")
                    {
                        allusersWithRoles1.Add(item);
                    }
                    else if (item1.Name == "Trainee")
                    {
                        allusersWithRoles2.Add(item);
                    }
                }
            }
            ViewBag.TrainerId = new SelectList(allusersWithRoles1, "UserId", "UserName");
            ViewBag.TraineeId = new SelectList(allusersWithRoles2, "UserId", "UserName");
            return View(cource);
        }

        // POST: Cources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleClaimsAuthorize("Cources", "Edit")]
        public ActionResult Edit([Bind(Include = "Id,Name,SysllabusLink,CourceCategoryId")] Cource cource)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cource).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourceCategoryId = new SelectList(db.CourceCategorys, "Id", "Name", cource.CourceCategoryId);
            return View(cource);
        }

        // GET: Cources/Delete/5
        [RoleClaimsAuthorize("Cources", "Delete")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cource cource = db.Cources.Find(id);
            if (cource == null)
            {
                return HttpNotFound();
            }
            return View(cource);
        }

        // POST: Cources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleClaimsAuthorize("Cources", "Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Cource cource = db.Cources.Find(id);
            db.Cources.Remove(cource);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [RoleClaimsAuthorize("Cources", "Delete")]
        public ActionResult DeleteTrainer(string id, int idCource)
        {
            var output =  db.TrainerInCource.Where(x=> x.CourceId == idCource && x.TrainerId == id).ToList();
            foreach (var item in output)
            {
                db.TrainerInCource.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = idCource });
        }
        [RoleClaimsAuthorize("Cources", "Delete")]
        public ActionResult DeleteTrainee(string id, int idCource)
        {
            var output = db.TraineeInCource.Where(x => x.CourceId == idCource && x.TraineeId == id).ToList();
            foreach (var item in output)
            {
                db.TraineeInCource.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = idCource });
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
