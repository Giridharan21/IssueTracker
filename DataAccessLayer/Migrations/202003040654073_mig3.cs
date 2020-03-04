namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Emp_FK", "dbo.EmpInfoes");
            DropIndex("dbo.Comments", new[] { "Emp_FK" });
            AlterColumn("dbo.Comments", "Emp_FK", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "Emp_FK");
            AddForeignKey("dbo.Comments", "Emp_FK", "dbo.EmpInfoes", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Emp_FK", "dbo.EmpInfoes");
            DropIndex("dbo.Comments", new[] { "Emp_FK" });
            AlterColumn("dbo.Comments", "Emp_FK", c => c.Int());
            CreateIndex("dbo.Comments", "Emp_FK");
            AddForeignKey("dbo.Comments", "Emp_FK", "dbo.EmpInfoes", "Id");
        }
    }
}
