using MatRoleClaim.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatRoleClaim.Models
{
    public class Course
    {
        public Course()
        {
            TrainerInCource = new HashSet<TrainerInCource>();
            TraineeInCource = new HashSet<TraineeInCource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SysllabusLink { get; set; }
        public int? CourceCategoryId { get; set; }
        public virtual CourseCategory CourceCategory { get; set; }
        public virtual ICollection<TrainerInCource> TrainerInCource { get; set; }
        public virtual ICollection<TraineeInCource> TraineeInCource { get; set; }
    }
}