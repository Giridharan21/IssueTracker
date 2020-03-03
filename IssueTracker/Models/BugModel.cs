using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
namespace IssueTracker.Models
{
    
    public class BugModel
    {
        [Required]
        public string Description  { get; set; }
        [Required]
        [Range(1,4,ErrorMessage ="Select Priority")]
        public Priority Prior  { get; set; }
        
    }
}