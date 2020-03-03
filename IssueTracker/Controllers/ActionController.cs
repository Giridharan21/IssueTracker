using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.DBContextClass;
using IssueTracker.Models;

namespace IssueTracker.Controllers
{
    public class ActionController : Controller
    {
        // GET: Action
        //public ActionResult Check(int id)
        //{
        //    var Emp = (EmpInfo)Session["User"];
        //    ViewBag.User = Emp;

        //    ViewBag.BugId = id;
        //    return View();
        //}
        public ActionResult Check() {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;

            //ViewBag.BugId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Check(int Id, string Status) {
            
            string Message = "";
            if (Status == "Resolved") {
                Message = "Closed the Bug Successfully!";
                Status = "Closed";
            }
            else if (Status == "Closed") {
                Message = "Bug Re-Opened Again..!";
                Status = "Re-Open";
            }
            else{
                Message = "Invalid Operation.. Bug is not yet Resolved";
            }
            Session["Msg"] = "alert('" + Message + "')";
            Data.Change(Id, Status);
            return Redirect("~/Authenticate/Index");
        }

        
        public ActionResult Create() {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;

            return View();
        }
        [HttpPost]
        public ActionResult Create(BugModel bug) {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;
            if (ModelState.IsValid) {
                Data.Create(Emp, bug.Description, bug.Prior);
                return Redirect("~/Authenticate/Index");

            }
            return View();
        }


        
        public ActionResult Assign() {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;
            var obj = new AssignModel();
            var list = Data.SetDevelopers();
            obj.Emp_List = new SelectList(list);
            return View(obj);
        }
        [HttpPost]
        public ActionResult Assign(AssignModel assign) {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;
            ViewBag.val = assign.Emp_Id;
            var obj = new AssignModel();
            var list = Data.SetDevelopers();
            obj.Emp_List = new SelectList(list);

            return View(obj);
        }

        public ActionResult Resolve(int Id) {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;
            Data.Change(Id, "Resolved");
            return View();
        }
    }
}