namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fk_Change : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssignedInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Emp_FK = c.Int(nullable: false),
                        Project_FK = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmpInfoes", t => t.Emp_FK, cascadeDelete: true)
                .ForeignKey("dbo.ProjectInfoes", t => t.Project_FK, cascadeDelete: true)
                .Index(t => t.Emp_FK)
                .Index(t => t.Project_FK);
            
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
                        Description = c.String(),
                        Project_FK = c.Int(nullable: false),
                        RaisedBy_FK = c.Int(nullable: false),
                        AssignedTo_FK = c.Int(nullable: false),
                        Status = c.String(),
                        Priority = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmpInfoes", t => t.AssignedTo_FK, cascadeDelete: false)
                .ForeignKey("dbo.ProjectInfoes", t => t.Project_FK, cascadeDelete: true)
                .ForeignKey("dbo.EmpInfoes", t => t.RaisedBy_FK, cascadeDelete: true)
                .Index(t => t.AssignedTo_FK)
                .Index(t => t.Project_FK)
                .Index(t => t.RaisedBy_FK);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Bugs_FK = c.Int(nullable: false),
                        Comment = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BugPools", t => t.Bugs_FK, cascadeDelete: true)
                .Index(t => t.Bugs_FK);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Bugs_FK", "dbo.BugPools");
            DropForeignKey("dbo.BugPools", "RaisedBy_FK", "dbo.EmpInfoes");
            DropForeignKey("dbo.BugPools", "Project_FK", "dbo.ProjectInfoes");
            DropForeignKey("dbo.BugPools", "AssignedTo_FK", "dbo.EmpInfoes");
            DropForeignKey("dbo.AssignedInfoes", "Project_FK", "dbo.ProjectInfoes");
            DropForeignKey("dbo.AssignedInfoes", "Emp_FK", "dbo.EmpInfoes");
            DropIndex("dbo.Comments", new[] { "Bugs_FK" });
            DropIndex("dbo.BugPools", new[] { "RaisedBy_FK" });
            DropIndex("dbo.BugPools", new[] { "Project_FK" });
            DropIndex("dbo.BugPools", new[] { "AssignedTo_FK" });
            DropIndex("dbo.AssignedInfoes", new[] { "Project_FK" });
            DropIndex("dbo.AssignedInfoes", new[] { "Emp_FK" });
            DropTable("dbo.Comments");
            DropTable("dbo.BugPools");
            DropTable("dbo.ProjectInfoes");
            DropTable("dbo.EmpInfoes");
            DropTable("dbo.AssignedInfoes");
        }
    }
}
