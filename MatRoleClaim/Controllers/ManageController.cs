using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MatRoleClaim.Models;
using MatRoleClaim.Models.ViewModels;
using MatRoleClaim.Models.IdentityModels;
using System.Net;

namespace MatRoleClaim.Controllers
{
    [Authorize]
    public class ManageController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(ManageMessageId? message, int? idcouse)
        {
            if (idcouse != null && idcouse != 0)
            {
                ViewBag.DataCouser = db.Courses.Find(idcouse);
            }
            else
            {
                ViewBag.DataCouser = null;
            }
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";

            var userId = User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword()
            };
            ViewBag.Trainer = db.TrainerInCource.Where(x => x.TrainerId == userId);
            ViewBag.Trainee = db.TraineeInCource.Where(x => x.TraineeId == userId);
            ViewBag.TrainerCount = db.TrainerInCource.Where(x => x.TrainerId == userId).Count();
            ViewBag.TraineeCount = db.TraineeInCource.Where(x => x.TraineeId == userId).Count();
            return View(model);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        #endregion

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

            ApplicationUserViewModel applicationUserViewModel = new ApplicationUserViewModel { Id = applicationUser.Id, Email = applicationUser.Email, NewPassword = "", UserName = applicationUser.UserName, DateOfBirth = applicationUser.DateOfBirth, Phone = applicationUser.Phone, Name = applicationUser.Name, Address = applicationUser.Address };
            return View(applicationUserViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Email,NewPassword,UserName,DateOfBirth,Phone,Address,Name")] ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser applicationUser = DbContext.Users.Find(applicationUserViewModel.Id);
                applicationUser.UserName = applicationUserViewModel.UserName;
                applicationUser.Phone = applicationUserViewModel.Phone;
                applicationUser.DateOfBirth = applicationUserViewModel.DateOfBirth;
                applicationUser.Name = applicationUserViewModel.Name;
                applicationUser.Address = applicationUserViewModel.Address;
                if (applicationUser == null)
                    return HttpNotFound();

                IdentityResult result = null;
                if (!String.IsNullOrEmpty(applicationUserViewModel.NewPassword))
                {
                    var token = await UserManager.GeneratePasswordResetTokenAsync(applicationUser.Id);
                    result = await UserManager.ResetPasswordAsync(applicationUser.Id, token, applicationUserViewModel.NewPassword);
                }
                var result1 = await UserManager.UpdateAsync(applicationUser);
                return Redirect("/Manage");
            }

            return View(applicationUserViewModel);
        }
    }
}