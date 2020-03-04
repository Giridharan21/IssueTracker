namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ColInComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Emp_FK", c => c.Int(nullable: true));
            CreateIndex("dbo.Comments", "Emp_FK");
            AddForeignKey("dbo.Comments", "Emp_FK", "dbo.EmpInfoes", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Emp_FK", "dbo.EmpInfoes");
            DropIndex("dbo.Comments", new[] { "Emp_FK" });
            DropColumn("dbo.Comments", "Emp_FK");
        }
    }
}
