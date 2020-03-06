using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccessLayer;
using DataAccessLayer.DBContextClass;
using IssueTracker.Models;
//hi
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


        [HttpGet]
        public ActionResult Assign(int Id=0) {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;
            var obj = new AssignModel();
            var list = Data.SetDevelopers(Emp);
            obj.Emp_List = new SelectList(list);
            ViewBag.Id = Id;
            if (Data.GetBugStatus(Id) == "Assigned") {
                Session["Msg"] = "alert('Task is already assigned...!')";
                return Redirect("~/Authenticate/Index");
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult Assign(AssignModel assign) {

            var Emp = (EmpInfo)Session["User"];
            var obj = new AssignModel();
            ViewBag.User = Emp;
            var list = Data.SetDevelopers(Emp);
            obj.Emp_List = new SelectList(list);
            if (ModelState.IsValid) {
                var DevId = assign.Emp_Id;
                int DevIdInt = int.Parse(DevId.Substring(DevId.IndexOf('(') + 1, DevId.IndexOf(')') - DevId.IndexOf('(') - 1));
                Data.Assign(Emp, assign.BugId, DevIdInt, assign.Comment);
                return RedirectToAction( "Index", "Authenticate");
            }
            ViewBag.Id = assign.BugId;
            return View(obj);
        }

        
        [HttpPost]
        public ActionResult Resolve(int Id,string Status) {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;
            string Message;
            if (Status == "Assigned" ) {
                Message = "The Bug is Resolved";
                Data.Change(Id, "Resolved");
            }
            else if (Status == "Closed") {
                Message = "The Bug is Closed.!";
            }
            
            else {
                Message = "Invalid Operation.. Bug is already Resolved";
            }
            Session["Msg"] = "alert('" + Message + "')";

            return Redirect("~/Authenticate/Index");
        }
        [HttpGet]
        public ActionResult Comments(int Id=0) {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User = Emp;
            ViewBag.BugId = Id;
            ViewBag.Count = "";
            var CmtList = Data.GetCommentsList(Id);
            if (CmtList.Count == 0)
                ViewBag.Count = "No List";
            return View(CmtList);
        }
        [HttpPost]
        public ActionResult Comments(int BugId,string Comment) {
            var Emp = (EmpInfo)Session["User"];
            ViewBag.Count = "";
            ViewBag.User = Emp;
            if (BugId != 0) {
                Data.AddComments(Emp, BugId, Comment);
                return Redirect("~/Authenticate/Index");
            }
            Session["Msg"] = "alert('Error Select A Bug To Add Comment...!')";
            return Redirect("~/Authenticate/Index");
        }
    }
}
