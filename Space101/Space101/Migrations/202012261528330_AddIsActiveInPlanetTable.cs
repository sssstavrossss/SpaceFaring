namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsActiveInPlanetTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Planets", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Planets", "IsActive");
        }
    }
}
