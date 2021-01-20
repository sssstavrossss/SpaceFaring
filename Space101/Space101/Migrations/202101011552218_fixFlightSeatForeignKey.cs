namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixFlightSeatForeignKey : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FlightSeats", new[] { "TravelClass_ClassId" });
            DropColumn("dbo.FlightSeats", "TravelClassId");
            RenameColumn(table: "dbo.FlightSeats", name: "TravelClass_ClassId", newName: "TravelClassId");
            AlterColumn("dbo.FlightSeats", "TravelClassId", c => c.String(maxLength: 10));
            CreateIndex("dbo.FlightSeats", "TravelClassId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.FlightSeats", new[] { "TravelClassId" });
            AlterColumn("dbo.FlightSeats", "TravelClassId", c => c.String());
            RenameColumn(table: "dbo.FlightSeats", name: "TravelClassId", newName: "TravelClass_ClassId");
            AddColumn("dbo.FlightSeats", "TravelClassId", c => c.String());
            CreateIndex("dbo.FlightSeats", "TravelClass_ClassId");
        }
    }
}
