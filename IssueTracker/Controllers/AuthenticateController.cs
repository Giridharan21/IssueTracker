using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IssueTracker.Models;
using DataAccessLayer;
using DataAccessLayer.DBContextClass;
namespace IssueTracker.Controllers
{
    public class AuthenticateController : Controller
    {
        // GET: Authenticate
        public ActionResult Login() {
            ViewBag.alert = "";
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel user) {
            var Emp = Data.Authenticate(user.Username, user.Password);
            if(ModelState.IsValid&&(!(Emp is null))) {
                Session["User"] = Emp;
                return Redirect("~/Authenticate/Index");
            }
            ViewBag.alert = "alert('Invalid User')";
            return View();
        }
        [HttpGet]
        public ActionResult Index() 
        {
            
            var Emp = (EmpInfo)Session["User"];
            ViewBag.User =Emp;
            ViewBag.List = null;
            var msg = (string)Session["Msg"];
            if(msg!="")
                ViewBag.Msg = "alert("+msg+")";
            if (!(Emp is null)) {
                var list = Data.GetBugList(Emp);
                ViewBag.List = list;
                
            }
            
            return View();
        }
    }
}