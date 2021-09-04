using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MatRoleClaim.Models
{
    public class CourceCategory
    {
        public CourceCategory()
        {
            Cources = new HashSet<Cource>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Cource> Cources { get; set; }
    }
}