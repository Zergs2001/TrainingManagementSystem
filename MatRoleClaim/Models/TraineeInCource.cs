using MatRoleClaim.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MatRoleClaim.Models
{
    public class TraineeInCource
    {
        [Key]
        [Column(Order = 0)]
        public string TraineeId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int CourceId { get; set; }
        public virtual Cource CourceTrainee { get; set; }
        public virtual ApplicationUser UserTrainees { get; set; }
    }
}