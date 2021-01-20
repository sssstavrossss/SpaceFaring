namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingRaceModelAndIntermediateTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersHomeplanets", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersHomeplanets", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.UsersRaces", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.UsersRaces", "RaceID", "dbo.Races");
            DropIndex("dbo.UsersHomeplanets", new[] { "ApplicationUserID" });
            DropIndex("dbo.UsersHomeplanets", new[] { "PlanetId" });
            DropIndex("dbo.UsersRaces", new[] { "ApplicationUserID" });
            DropIndex("dbo.UsersRaces", new[] { "RaceID" });
            AddColumn("dbo.AspNetUsers", "PlanetID", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "RaceID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "PlanetID");
            CreateIndex("dbo.AspNetUsers", "RaceID");
            AddForeignKey("dbo.AspNetUsers", "PlanetID", "dbo.Planets", "PlanetID");
            AddForeignKey("dbo.AspNetUsers", "RaceID", "dbo.Races", "RaceID");
            DropTable("dbo.UsersHomeplanets");
            DropTable("dbo.UsersRaces");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersRaces",
                c => new
                    {
                        ApplicationUserID = c.String(nullable: false, maxLength: 128),
                        RaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserID);
            
            CreateTable(
                "dbo.UsersHomeplanets",
                c => new
                    {
                        ApplicationUserID = c.String(nullable: false, maxLength: 128),
                        PlanetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ApplicationUserID);
            
            DropForeignKey("dbo.AspNetUsers", "RaceID", "dbo.Races");
            DropForeignKey("dbo.AspNetUsers", "PlanetID", "dbo.Planets");
            DropIndex("dbo.AspNetUsers", new[] { "RaceID" });
            DropIndex("dbo.AspNetUsers", new[] { "PlanetID" });
            DropColumn("dbo.AspNetUsers", "RaceID");
            DropColumn("dbo.AspNetUsers", "PlanetID");
            CreateIndex("dbo.UsersRaces", "RaceID");
            CreateIndex("dbo.UsersRaces", "ApplicationUserID");
            CreateIndex("dbo.UsersHomeplanets", "PlanetId");
            CreateIndex("dbo.UsersHomeplanets", "ApplicationUserID");
            AddForeignKey("dbo.UsersRaces", "RaceID", "dbo.Races", "RaceID", cascadeDelete: true);
            AddForeignKey("dbo.UsersRaces", "ApplicationUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UsersHomeplanets", "PlanetId", "dbo.Planets", "PlanetID", cascadeDelete: true);
            AddForeignKey("dbo.UsersHomeplanets", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
    }
}
