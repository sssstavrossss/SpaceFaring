namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRaceAvatar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Races", "Avatar", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Races", "Avatar");
        }
    }
}
