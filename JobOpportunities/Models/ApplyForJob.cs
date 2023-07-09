using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOpportunities.Models
{
    public class ApplyForJob
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Resume { get; set; }
        public DateTime ApplyDate { get; set; }

        // FK
        public int JobId { get; set; }
        public string UserId { get; set; }



        // Navigation properties
        public virtual Job Job { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}