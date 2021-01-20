namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RaceFileManager : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RaceFiles",
                c => new
                    {
                        FilePathId = c.Int(nullable: false),
                        RaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilePathId, t.RaceID })
                .ForeignKey("dbo.FilePaths", t => t.FilePathId, cascadeDelete: true)
                .ForeignKey("dbo.Races", t => t.RaceID, cascadeDelete: true)
                .Index(t => t.FilePathId)
                .Index(t => t.RaceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RaceFiles", "RaceID", "dbo.Races");
            DropForeignKey("dbo.RaceFiles", "FilePathId", "dbo.FilePaths");
            DropIndex("dbo.RaceFiles", new[] { "RaceID" });
            DropIndex("dbo.RaceFiles", new[] { "FilePathId" });
            DropTable("dbo.RaceFiles");
        }
    }
}
