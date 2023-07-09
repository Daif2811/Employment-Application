using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace JobOpportunities.Models
{
    public class Job
    {

        [Required]
        public int JobID { get; set; }



        [Required, Display(Name ="Job Name")]
        public string JobTitle { get; set; }

        [AllowHtml]
        [Required, Display(Name = "Job Description")]
        public string JobContent { get; set; }


        [Display(Name = "Job Image")]
        public string JobImage { get; set; }


        [Required, Display(Name = "Job Type")]
        public int CategoryID { get; set; }

        public string UserId { get; set; }



        public virtual ICollection<ApplyForJob> ApplyForJobs { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual Category Category { get; set; }

    }
}