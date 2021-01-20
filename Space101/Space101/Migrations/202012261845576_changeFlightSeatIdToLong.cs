namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeFlightSeatIdToLong : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.FlightSeats");
            AlterColumn("dbo.FlightSeats", "FlightSeatId", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.FlightSeats", "FlightSeatId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.FlightSeats");
            AlterColumn("dbo.FlightSeats", "FlightSeatId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.FlightSeats", "FlightSeatId");
        }
    }
}
