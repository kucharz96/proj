namespace Projekt1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xyz1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Specialization",
                c => new
                    {
                        SpecializationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SpecializationId);
            
            AddColumn("dbo.Doctor", "Specializations_SpecializationId", c => c.Int());
            CreateIndex("dbo.Doctor", "Specializations_SpecializationId");
            AddForeignKey("dbo.Doctor", "Specializations_SpecializationId", "dbo.Specialization", "SpecializationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Doctor", "Specializations_SpecializationId", "dbo.Specialization");
            DropIndex("dbo.Doctor", new[] { "Specializations_SpecializationId" });
            DropColumn("dbo.Doctor", "Specializations_SpecializationId");
            DropTable("dbo.Specialization");
        }
    }
}
