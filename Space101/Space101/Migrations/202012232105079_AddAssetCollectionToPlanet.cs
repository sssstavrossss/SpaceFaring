namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAssetCollectionToPlanet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanetFiles",
                c => new
                    {
                        FilePathId = c.Int(nullable: false),
                        PlanetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilePathId, t.PlanetId })
                .ForeignKey("dbo.FilePaths", t => t.FilePathId, cascadeDelete: true)
                .ForeignKey("dbo.Planets", t => t.PlanetId, cascadeDelete: true)
                .Index(t => t.FilePathId)
                .Index(t => t.PlanetId);
            
            CreateTable(
                "dbo.FilePaths",
                c => new
                    {
                        FilePathId = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileType = c.Int(nullable: false),
                        FileExtencion = c.String(),
                    })
                .PrimaryKey(t => t.FilePathId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanetFiles", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.PlanetFiles", "FilePathId", "dbo.FilePaths");
            DropIndex("dbo.PlanetFiles", new[] { "PlanetId" });
            DropIndex("dbo.PlanetFiles", new[] { "FilePathId" });
            DropTable("dbo.FilePaths");
            DropTable("dbo.PlanetFiles");
        }
    }
}
