namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTerrainTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurfaceMorphologies",
                c => new
                    {
                        PlanetId = c.Int(nullable: false),
                        TerrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlanetId, t.TerrainId })
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .ForeignKey("dbo.Terrains", t => t.TerrainId, cascadeDelete: true)
                .Index(t => t.PlanetId)
                .Index(t => t.TerrainId);
            
            CreateTable(
                "dbo.Terrains",
                c => new
                    {
                        TerrainId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TerrainId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurfaceMorphologies", "TerrainId", "dbo.Terrains");
            DropForeignKey("dbo.SurfaceMorphologies", "PlanetId", "dbo.Planets");
            DropIndex("dbo.SurfaceMorphologies", new[] { "TerrainId" });
            DropIndex("dbo.SurfaceMorphologies", new[] { "PlanetId" });
            DropTable("dbo.Terrains");
            DropTable("dbo.SurfaceMorphologies");
        }
    }
}
