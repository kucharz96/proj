namespace Projekt1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xyz2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "UserKey", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "UserKey", c => c.String());
        }
    }
}
