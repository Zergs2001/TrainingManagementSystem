using MatRoleClaim.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MatRoleClaim.Models
{
    public class TrainerInCource
    {
        [Key]
        [Column(Order = 0)]
        public string TrainerId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int CourceId { get; set; }
        public virtual Course Cource { get; set; }
        public virtual ApplicationUser Trainer { get; set; }
    }
}