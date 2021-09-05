using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatRoleClaim.Models
{
    public class CourseCategory
    {
        public CourseCategory()
        {
            Cources = new HashSet<Course>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Course> Cources { get; set; }
    }
}