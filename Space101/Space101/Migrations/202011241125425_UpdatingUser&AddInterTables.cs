namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingUserAddInterTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsersHomeplanets",
                c => new
                    {
                        ApplicationUserID = c.String(nullable: false, maxLength: 128),
                        PlanetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .Index(t => t.ApplicationUserID)
                .Index(t => t.PlanetId);
            
            CreateTable(
                "dbo.UsersRaces",
                c => new
                    {
                        ApplicationUserID = c.String(nullable: false, maxLength: 128),
                        RaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserID)
                .ForeignKey("dbo.Races", t => t.RaceID, cascadeDelete: true)
                .Index(t => t.ApplicationUserID)
                .Index(t => t.RaceID);
            
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.AspNetUsers", "NickName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersRaces", "RaceID", "dbo.Races");
            DropForeignKey("dbo.UsersRaces", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersHomeplanets", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.UsersHomeplanets", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.UsersRaces", new[] { "RaceID" });
            DropIndex("dbo.UsersRaces", new[] { "ApplicationUserID" });
            DropIndex("dbo.UsersHomeplanets", new[] { "PlanetId" });
            DropIndex("dbo.UsersHomeplanets", new[] { "ApplicationUserID" });
            DropColumn("dbo.AspNetUsers", "NickName");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropTable("dbo.UsersRaces");
            DropTable("dbo.UsersHomeplanets");
        }
    }
}
