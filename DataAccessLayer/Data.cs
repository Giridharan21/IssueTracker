using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DBContextClass;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DataAccessLayer
{
    public enum Priority {
        Low =1 ,
        Medium,
        High,
        VeryHigh
    }
    public enum Developer {
        Select= 0
    }
    public class Data
    {
        public static EmpInfo Authenticate (string name, string pass) {
            IssueTrackerModel context = new IssueTrackerModel();
            var Emp = context.Employees.Where(g => g.Name == name && g.Password == pass).FirstOrDefault();
            return Emp;
        }
        public static List<BugPool> GetBugList(EmpInfo Emp) {
            IssueTrackerModel context = new IssueTrackerModel();
            var BugQuery = context.Bugs.Where(g => g.BugInProject.Id == 0);
            List<BugPool> list = new List<BugPool>();
            if (Emp.Role == "PM") {
                var temp = context.Assigned.Where(g => g.Emp.Id == Emp.Id).Select(g=>g.Project).FirstOrDefault();
                BugQuery = context.Bugs.Where(g => g.BugInProject.Id == temp.Id).Select(g=>g);
                
            }
            else if (Emp.Role == "Tester") {
                BugQuery = context.Bugs.Where(g => g.RaisedBy.Id == Emp.Id);
            }
            else if(Emp.Role == "Developer") {
                BugQuery = context.Bugs.Where(g => g.AssignedTo.Id == Emp.Id);
            }
            var Project = context.Bugs.Select(g => g.BugInProject);
            foreach (var i in Project) { }
            var RaisedBy = context.Bugs.Select(g => g.RaisedBy);
            foreach (var i in RaisedBy) { }
            var AssignedTo = context.Bugs.Select(g => g.AssignedTo);
            foreach (var i in AssignedTo) { }
            foreach (var i in BugQuery)
                    list.Add(i);
            return list;
        }

        public static void Change(int id ,string Status) {
            IssueTrackerModel context = new IssueTrackerModel();
            var Bug = context.Bugs.Where(g => g.Id == id).FirstOrDefault();
            Bug.Status = Status;
            context.SaveChanges();
        }

        public static void Create(EmpInfo Emp, string Desc,  Priority x) {
            IssueTrackerModel context = new IssueTrackerModel();
            var project = context.Assigned.Where(g => g.Emp.Id == Emp.Id).Select(g => g.Project).FirstOrDefault();
            BugPool bug = new BugPool();
            bug.Priority = x.ToString();
            bug.RaisedBy_FK = Emp.Id;
            //bug.RaisedBy = Emp;
            bug.Status = "Open";
            bug.Project_FK = project.Id;
            //bug.BugInProject = project;
            bug.Description = Desc;
            context.Bugs.Add(bug);
            context.SaveChanges();
        }

        public static List<string> SetDevelopers() {
            IssueTrackerModel context = new IssueTrackerModel();
            var list = new List<string>();
            var devs = context.Employees.Where(g => g.Role == "Developer").Select(g => g);
            foreach(var i in devs) {
                list.Add(i.Id.ToString());
            }
            return list;
        }
    }
    
}
