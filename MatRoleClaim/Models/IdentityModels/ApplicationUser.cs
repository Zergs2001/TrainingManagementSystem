using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MatRoleClaim.Models.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            TrainerInCource = new HashSet<TrainerInCource>();
            CourceTrainees = new HashSet<Cource>();
        }
        public ICollection<Blog> Blogs { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<TrainerInCource> TrainerInCource { get; set; }
        public virtual ICollection<Cource> CourceTrainees { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}