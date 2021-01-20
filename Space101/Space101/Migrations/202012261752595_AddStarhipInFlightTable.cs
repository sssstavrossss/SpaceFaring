namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStarhipInFlightTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flights", "StarshipId", c => c.Int(nullable: false));
            CreateIndex("dbo.Flights", "StarshipId");
            AddForeignKey("dbo.Flights", "StarshipId", "dbo.Starships", "StarshipId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "StarshipId", "dbo.Starships");
            DropIndex("dbo.Flights", new[] { "StarshipId" });
            DropColumn("dbo.Flights", "StarshipId");
        }
    }
}
