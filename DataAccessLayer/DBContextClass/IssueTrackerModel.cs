namespace DataAccessLayer.DBContextClass
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class IssueTrackerModel : DbContext
    {
        // Your context has been configured to use a 'IssueTrackerModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccessLayer.DBContextClass.IssueTrackerModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'IssueTrackerModel' 
        // connection string in the application configuration file.
        public IssueTrackerModel()
            : base("name=IssueTracker") {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
         public virtual DbSet<EmpInfo> Employees { get; set; }
         public virtual DbSet<ProjectInfo> Projects { get; set; }
         public virtual DbSet<AssignedInfo> Assigned { get; set; }
         public virtual DbSet<BugPool> Bugs { get; set; }
         public virtual DbSet<Comments> Comments { get; set; }
    }
    public class EmpInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
    public class ProjectInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
         
    }
    public class AssignedInfo
    {
        public int Id { get; set; }
        public EmpInfo Emp { get; set; }
        public ProjectInfo Project { get; set; }

    }
    public class BugPool 
    {
        public int Id { get; set; }
        public ProjectInfo BugInProject { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public EmpInfo RaisedBy { get; set; }
        public EmpInfo AssignedTo { get; set; }

    }
    public class Comments
    {
        public int Id { get; set; }
        public ProjectInfo CurrentProject { get; set; }
        public string Comment { get; set; }
        public EmpInfo CommentBy { get; set; }
        public DateTime  Date { get; set; }
    }
}