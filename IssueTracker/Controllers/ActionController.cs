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
        public ActionResult Check(int id)
        {
            ViewBag.BugId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Check(int Id, string Status) {
            Data.Change(Id, Status);
            return Redirect("~/Authenticate/Index");
        }
        public ActionResult Create() {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BugModel bug) {
            var Emp = (EmpInfo)Session["User"];
            Data.Create(Emp, bug.Description, bug.Priority);
            return View("~/Authenticate/index");
        }
        public ActionResult Assign() {
            return View();
        }
        public ActionResult Resolve() {
            return View();
        }
    }
}