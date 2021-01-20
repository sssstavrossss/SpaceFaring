namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddClimateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClimateZones",
                c => new
                    {
                        PlanetId = c.Int(nullable: false),
                        ClimateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanetId, t.ClimateId })
                .ForeignKey("dbo.Climates", t => t.ClimateId, cascadeDelete: true)
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .Index(t => t.PlanetId)
                .Index(t => t.ClimateId);
            
            CreateTable(
                "dbo.Climates",
                c => new
                    {
                        ClimateId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ClimateId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClimateZones", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.ClimateZones", "ClimateId", "dbo.Climates");
            DropIndex("dbo.ClimateZones", new[] { "ClimateId" });
            DropIndex("dbo.ClimateZones", new[] { "PlanetId" });
            DropTable("dbo.Climates");
            DropTable("dbo.ClimateZones");
        }
    }
}
