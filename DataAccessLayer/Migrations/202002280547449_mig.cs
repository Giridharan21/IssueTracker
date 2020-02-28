namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Emp_Id = c.Int(),
                        Project_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmpInfoes", t => t.Emp_Id)
                .ForeignKey("dbo.ProjectInfoes", t => t.Project_Id)
                .Index(t => t.Emp_Id)
                .Index(t => t.Project_Id);
            
            CreateTable(
                "dbo.EmpInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        Role = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BugPools",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                        Priority = c.String(),
                        AssignedTo_Id = c.Int(),
                        BugInProject_Id = c.Int(),
                        RaisedBy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmpInfoes", t => t.AssignedTo_Id)
                .ForeignKey("dbo.ProjectInfoes", t => t.BugInProject_Id)
                .ForeignKey("dbo.EmpInfoes", t => t.RaisedBy_Id)
                .Index(t => t.AssignedTo_Id)
                .Index(t => t.BugInProject_Id)
                .Index(t => t.RaisedBy_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Date = c.DateTime(nullable: false),
                        CommentBy_Id = c.Int(),
                        CurrentProject_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmpInfoes", t => t.CommentBy_Id)
                .ForeignKey("dbo.ProjectInfoes", t => t.CurrentProject_Id)
                .Index(t => t.CommentBy_Id)
                .Index(t => t.CurrentProject_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "CurrentProject_Id", "dbo.ProjectInfoes");
            DropForeignKey("dbo.Comments", "CommentBy_Id", "dbo.EmpInfoes");
            DropForeignKey("dbo.BugPools", "RaisedBy_Id", "dbo.EmpInfoes");
            DropForeignKey("dbo.BugPools", "BugInProject_Id", "dbo.ProjectInfoes");
            DropForeignKey("dbo.BugPools", "AssignedTo_Id", "dbo.EmpInfoes");
            DropForeignKey("dbo.AssignedInfoes", "Project_Id", "dbo.ProjectInfoes");
            DropForeignKey("dbo.AssignedInfoes", "Emp_Id", "dbo.EmpInfoes");
            DropIndex("dbo.Comments", new[] { "CurrentProject_Id" });
            DropIndex("dbo.Comments", new[] { "CommentBy_Id" });
            DropIndex("dbo.BugPools", new[] { "RaisedBy_Id" });
            DropIndex("dbo.BugPools", new[] { "BugInProject_Id" });
            DropIndex("dbo.BugPools", new[] { "AssignedTo_Id" });
            DropIndex("dbo.AssignedInfoes", new[] { "Project_Id" });
            DropIndex("dbo.AssignedInfoes", new[] { "Emp_Id" });
            DropTable("dbo.Comments");
            DropTable("dbo.BugPools");
            DropTable("dbo.ProjectInfoes");
            DropTable("dbo.EmpInfoes");
            DropTable("dbo.AssignedInfoes");
        }
    }
}
