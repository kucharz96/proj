namespace Projekt1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xyz : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Voivodeship_VoivodeshipId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Voivodeship", t => t.Voivodeship_VoivodeshipId)
                .Index(t => t.Voivodeship_VoivodeshipId);
            
            CreateTable(
                "dbo.Voivodeship",
                c => new
                    {
                        VoivodeshipId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.VoivodeshipId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        IsEnable = c.Boolean(nullable: false),
                        UserKey = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Doctor",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        City_CityId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.City", t => t.City_CityId)
                .Index(t => t.UserId)
                .Index(t => t.City_CityId);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patient", "UserId", "dbo.User");
            DropForeignKey("dbo.Doctor", "City_CityId", "dbo.City");
            DropForeignKey("dbo.Doctor", "UserId", "dbo.User");
            DropForeignKey("dbo.City", "Voivodeship_VoivodeshipId", "dbo.Voivodeship");
            DropIndex("dbo.Patient", new[] { "UserId" });
            DropIndex("dbo.Doctor", new[] { "City_CityId" });
            DropIndex("dbo.Doctor", new[] { "UserId" });
            DropIndex("dbo.City", new[] { "Voivodeship_VoivodeshipId" });
            DropTable("dbo.Patient");
            DropTable("dbo.Doctor");
            DropTable("dbo.User");
            DropTable("dbo.Voivodeship");
            DropTable("dbo.City");
        }
    }
}
