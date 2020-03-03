using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IssueTracker.Models {
    public class AssignModel {
        
        public int BugId { get; set; }
        [Required(ErrorMessage ="Select a Developer")]
        public string Emp_Id { get; set; }
        public string Comment { get; set; }
        
        public SelectList Emp_List { get; set; }
        
    }
}