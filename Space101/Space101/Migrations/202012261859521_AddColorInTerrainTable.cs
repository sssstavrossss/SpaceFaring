namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColorInTerrainTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Terrains", "DisplayColor", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Terrains", "DisplayColor");
        }
    }
}
