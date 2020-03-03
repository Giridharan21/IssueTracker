namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bug : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BugPools", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BugPools", "Description");
        }
    }
}
