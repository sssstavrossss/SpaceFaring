namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesOnTicketAndOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.Tickets", "RaceId", "dbo.Races");
            DropIndex("dbo.Tickets", new[] { "Seat_FlightSeatId" });
            DropColumn("dbo.Tickets", "SeatId");
            RenameColumn(table: "dbo.Tickets", name: "Seat_FlightSeatId", newName: "SeatId");
            RenameColumn(table: "dbo.Tickets", name: "PlanetId", newName: "PassengerPlanetId");
            RenameColumn(table: "dbo.Tickets", name: "RaceId", newName: "PassengerRaceId");
            RenameIndex(table: "dbo.Tickets", name: "IX_RaceId", newName: "IX_PassengerRaceId");
            RenameIndex(table: "dbo.Tickets", name: "IX_PlanetId", newName: "IX_PassengerPlanetId");
            AddColumn("dbo.Tickets", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Tickets", "SeatId", c => c.Long(nullable: false));
            CreateIndex("dbo.Tickets", "SeatId");
            AddForeignKey("dbo.Tickets", "FlightId", "dbo.Flights", "FlightId");
            AddForeignKey("dbo.Tickets", "PassengerRaceId", "dbo.Races", "RaceID");
            DropColumn("dbo.Tickets", "BuyerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "BuyerId", c => c.String());
            DropForeignKey("dbo.Tickets", "PassengerRaceId", "dbo.Races");
            DropForeignKey("dbo.Tickets", "FlightId", "dbo.Flights");
            DropIndex("dbo.Tickets", new[] { "SeatId" });
            AlterColumn("dbo.Tickets", "SeatId", c => c.Long());
            DropColumn("dbo.Tickets", "Price");
            RenameIndex(table: "dbo.Tickets", name: "IX_PassengerPlanetId", newName: "IX_PlanetId");
            RenameIndex(table: "dbo.Tickets", name: "IX_PassengerRaceId", newName: "IX_RaceId");
            RenameColumn(table: "dbo.Tickets", name: "PassengerRaceId", newName: "RaceId");
            RenameColumn(table: "dbo.Tickets", name: "PassengerPlanetId", newName: "PlanetId");
            RenameColumn(table: "dbo.Tickets", name: "SeatId", newName: "Seat_FlightSeatId");
            AddColumn("dbo.Tickets", "SeatId", c => c.Long(nullable: false));
            CreateIndex("dbo.Tickets", "Seat_FlightSeatId");
            AddForeignKey("dbo.Tickets", "RaceId", "dbo.Races", "RaceID", cascadeDelete: true);
            AddForeignKey("dbo.Tickets", "FlightId", "dbo.Flights", "FlightId", cascadeDelete: true);
        }
    }
}
