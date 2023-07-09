using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobOpportunities.Models
{
    public class Category
    {
        [Required]
        public int CategoryID { get; set; }



        [Required, Display(Name ="Job Type")]
        public string CategoryName { get; set; }


        [Required, Display(Name = "Description Type")]
        public string CategoryDescription { get; set; }



        public virtual ICollection<Job> Jobs { get; set; }


    }
}