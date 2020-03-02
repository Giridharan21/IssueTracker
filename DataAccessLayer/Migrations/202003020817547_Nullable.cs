namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BugPools", "AssignedTo_FK", "dbo.EmpInfoes");
            DropIndex("dbo.BugPools", new[] { "AssignedTo_FK" });
            AlterColumn("dbo.BugPools", "AssignedTo_FK", c => c.Int(nullable:true));
            CreateIndex("dbo.BugPools", "AssignedTo_FK");
            AddForeignKey("dbo.BugPools", "AssignedTo_FK", "dbo.EmpInfoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BugPools", "AssignedTo_FK", "dbo.EmpInfoes");
            DropIndex("dbo.BugPools", new[] { "AssignedTo_FK" });
            AlterColumn("dbo.BugPools", "AssignedTo_FK", c => c.Int(nullable: false));
            CreateIndex("dbo.BugPools", "AssignedTo_FK");
            AddForeignKey("dbo.BugPools", "AssignedTo_FK", "dbo.EmpInfoes", "Id", cascadeDelete: true);
        }
    }
}
