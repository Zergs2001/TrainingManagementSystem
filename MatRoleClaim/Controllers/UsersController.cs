using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MatRoleClaim.Attributes;
using MatRoleClaim.Models;
using MatRoleClaim.Models.IdentityModels;
using MatRoleClaim.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace MatRoleClaim.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        [RoleClaimsAuthorize("Users", "Show")]
        public ActionResult Index()
        {
            return View(DbContext.Users.ToList());
        }

        [RoleClaimsAuthorize("Users", "Show")]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = DbContext.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [RoleClaimsAuthorize("Users", "Add")]
        public ActionResult Create()
        {
            ViewBag.Name = new SelectList(DbContext.Roles.ToList(), "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleClaimsAuthorize("Users", "Add")]
        public async Task<ActionResult> Create([Bind(Include = "UserName,UserRoles,Email,Password,ConfirmPassword,DateOfBirth,Phone")] RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = registerViewModel.Email, Email = registerViewModel.Email, DateOfBirth = registerViewModel.DateOfBirth, Phone = registerViewModel.Phone };
                var result = await UserManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, registerViewModel.UserRoles);
                    return Redirect("/UserRoles/ManageAccount");
                }
                AddErrors(result);
            }

            ViewBag.Name = new SelectList(DbContext.Roles.ToList(), "Name", "Name");
            return View(registerViewModel);
        }

        [RoleClaimsAuthorize("Users", "Edit")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = DbContext.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            ApplicationUserViewModel applicationUserViewModel = new ApplicationUserViewModel { Id = applicationUser.Id, Email = applicationUser.Email, NewPassword = "", UserName = applicationUser.UserName, DateOfBirth = applicationUser.DateOfBirth, Phone = applicationUser.Phone };
            return View(applicationUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RoleClaimsAuthorize("Users", "Edit")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,NewPassword,UserName,DateOfBirth,Phone")] ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser applicationUser = DbContext.Users.Find(applicationUserViewModel.Id);
                applicationUser.UserName = applicationUserViewModel.UserName;
                applicationUser.Phone = applicationUserViewModel.Phone;
                applicationUser.DateOfBirth = applicationUserViewModel.DateOfBirth;
                if (applicationUser == null)
                    return HttpNotFound();

                IdentityResult result = null;
                if (!String.IsNullOrEmpty(applicationUserViewModel.NewPassword))
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(applicationUser.Id);
                    result = await UserManager.ResetPasswordAsync(applicationUser.Id, token, applicationUserViewModel.NewPassword);
                }
                var result1 = await UserManager.UpdateAsync(applicationUser);
                return Redirect("/UserRoles/ManageAccount");
            }

            return View(applicationUserViewModel);
        }

        [RoleClaimsAuthorize("Users", "Delete")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = DbContext.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            return View(applicationUser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [RoleClaimsAuthorize("Users", "Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = DbContext.Users.Find(id);
            DbContext.Users.Remove(applicationUser);
            DbContext.SaveChanges();
            return Redirect("/UserRoles/ManageAccount");
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}
