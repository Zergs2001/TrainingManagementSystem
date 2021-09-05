using MatRoleClaim.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatRoleClaim.Models.ViewModels
{
    public class UserRolesViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}