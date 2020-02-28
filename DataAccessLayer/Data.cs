﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DBContextClass;
namespace DataAccessLayer
{
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
                var temp = context.Assigned.Where(g => g.Emp.Id == Emp.Id).FirstOrDefault();
                BugQuery = context.Bugs.Where(g => g.BugInProject.Id == temp.Id);
            }
            else if (Emp.Role == "Tester") {
                BugQuery = context.Bugs.Where(g => g.RaisedBy.Id == Emp.Id);
            }
            else if(Emp.Role == "Developer") {
                BugQuery = context.Bugs.Where(g => g.AssignedTo.Id == Emp.Id);
            }
            foreach (var i in BugQuery)
                list.Add(i);
            return list;
        }
    }
}
