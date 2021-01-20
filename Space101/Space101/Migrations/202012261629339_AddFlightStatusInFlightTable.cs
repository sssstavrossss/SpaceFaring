namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlightStatusInFlightTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flights", "FlightStatusId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Flights", "FlightStatusId");
            AddForeignKey("dbo.Flights", "FlightStatusId", "dbo.FlightStatus", "FlightStatusId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "FlightStatusId", "dbo.FlightStatus");
            DropIndex("dbo.Flights", new[] { "FlightStatusId" });
            DropColumn("dbo.Flights", "FlightStatusId");
        }
    }
}
