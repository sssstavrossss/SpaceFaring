namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvatarInPlanetTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Planets", "Title", c => c.String());
            AddColumn("dbo.Planets", "Avatar", c => c.Binary());
            DropColumn("dbo.Planets", "Url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Planets", "Url", c => c.String());
            DropColumn("dbo.Planets", "Avatar");
            DropColumn("dbo.Planets", "Title");
        }
    }
}
