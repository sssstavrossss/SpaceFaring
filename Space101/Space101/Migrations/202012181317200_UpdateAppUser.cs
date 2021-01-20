namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAppUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.AspNetUsers", new[] { "PlanetID" });
            DropIndex("dbo.AspNetUsers", new[] { "RaceID" });
            AlterColumn("dbo.AspNetUsers", "PlanetID", c => c.Int());
            AlterColumn("dbo.AspNetUsers", "RaceID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "PlanetID");
            CreateIndex("dbo.AspNetUsers", "RaceID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AspNetUsers", new[] { "RaceID" });
            DropIndex("dbo.AspNetUsers", new[] { "PlanetID" });
            AlterColumn("dbo.AspNetUsers", "RaceID", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PlanetID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "RaceID");
            CreateIndex("dbo.AspNetUsers", "PlanetID");
        }
    }
}
