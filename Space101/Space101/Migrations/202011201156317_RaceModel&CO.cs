namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RaceModelCO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaceClassifications",
                c => new
                    {
                        RaceClassificationID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.RaceClassificationID);
            
            CreateTable(
                "dbo.Races",
                c => new
                    {
                        RaceID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        RaceClassificationID = c.Int(nullable: false),
                        AverageHeight = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RaceID)
                .ForeignKey("dbo.RaceClassifications", t => t.RaceClassificationID, cascadeDelete: true)
                .Index(t => t.RaceClassificationID);

            CreateTable(
                "dbo.RaceHabitats",
                c => new
                {
                    PlanetID = c.Int(nullable: false),
                    RaceID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.PlanetID, t.RaceID })
                .ForeignKey("dbo.Planets", t => t.PlanetID, cascadeDelete: true)
                .ForeignKey("dbo.Races", t => t.RaceID, cascadeDelete: true)
                .Index(t => t.PlanetID)
                .Index(t => t.RaceID);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaceHabitats", "RaceID", "dbo.Races");
            DropForeignKey("dbo.RaceHabitats", "PlanetID", "dbo.Planets");
            DropForeignKey("dbo.Races", "RaceClassificationID", "dbo.RaceClassifications");
            DropIndex("dbo.RaceHabitats", new[] { "RaceID" });
            DropIndex("dbo.RaceHabitats", new[] { "PlanetID" });
            DropIndex("dbo.Races", new[] { "RaceClassificationID" });
            DropTable("dbo.RaceHabitats");
            DropTable("dbo.Races");
            DropTable("dbo.RaceClassifications");
        }
    }
}
