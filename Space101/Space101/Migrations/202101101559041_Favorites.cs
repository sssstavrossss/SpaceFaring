namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Favorites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserFavorites",
                c => new
                    {
                        UserFavoriteID = c.Int(nullable: false, identity: true),
                        FlightID = c.Int(nullable: false),
                        ApplicationUserID = c.Int(nullable: false),
                        ApplicationUsers_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserFavoriteID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUsers_Id)
                .ForeignKey("dbo.Flights", t => t.FlightID, cascadeDelete: true)
                .Index(t => t.FlightID)
                .Index(t => t.ApplicationUsers_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFavorites", "FlightID", "dbo.Flights");
            DropForeignKey("dbo.UserFavorites", "ApplicationUsers_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserFavorites", new[] { "ApplicationUsers_Id" });
            DropIndex("dbo.UserFavorites", new[] { "FlightID" });
            DropTable("dbo.UserFavorites");
        }
    }
}
