namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColorInClimateTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Climates", "DisplayColor", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Climates", "DisplayColor");
        }
    }
}
