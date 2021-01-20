namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlightSeatsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlightSeats",
                c => new
                    {
                        FlightSeatId = c.Int(nullable: false, identity: true),
                        FlightId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FlightSeatId)
                .ForeignKey("dbo.Flights", t => t.FlightId, cascadeDelete: true)
                .Index(t => t.FlightId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlightSeats", "FlightId", "dbo.Flights");
            DropIndex("dbo.FlightSeats", new[] { "FlightId" });
            DropTable("dbo.FlightSeats");
        }
    }
}
