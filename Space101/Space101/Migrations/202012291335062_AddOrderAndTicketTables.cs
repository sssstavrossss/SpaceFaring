namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderAndTicketTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Long(nullable: false, identity: true),
                        BuyerEmail = c.String(nullable: false),
                        UserId = c.String(maxLength: 128),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TicketOrders",
                c => new
                    {
                        OrderId = c.Long(nullable: false),
                        TicketId = c.Long(nullable: false),
                        Quantity = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.TicketId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        RaceId = c.Int(nullable: false),
                        SeatId = c.Long(nullable: false),
                        PlanetId = c.Int(nullable: false),
                        FlightId = c.Int(nullable: false),
                        BuyerId = c.String(),
                        Seat_FlightSeatId = c.Long(),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Flights", t => t.FlightId, cascadeDelete: true)
                .ForeignKey("dbo.Planets", t => t.PlanetId)
                .ForeignKey("dbo.Races", t => t.RaceId, cascadeDelete: true)
                .ForeignKey("dbo.FlightSeats", t => t.Seat_FlightSeatId)
                .Index(t => t.RaceId)
                .Index(t => t.PlanetId)
                .Index(t => t.FlightId)
                .Index(t => t.Seat_FlightSeatId);
            
            DropColumn("dbo.FlightSeats", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FlightSeats", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TicketOrders", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "Seat_FlightSeatId", "dbo.FlightSeats");
            DropForeignKey("dbo.Tickets", "RaceId", "dbo.Races");
            DropForeignKey("dbo.Tickets", "PlanetId", "dbo.Planets");
            DropForeignKey("dbo.Tickets", "FlightId", "dbo.Flights");
            DropForeignKey("dbo.TicketOrders", "OrderId", "dbo.Orders");
            DropIndex("dbo.Tickets", new[] { "Seat_FlightSeatId" });
            DropIndex("dbo.Tickets", new[] { "FlightId" });
            DropIndex("dbo.Tickets", new[] { "PlanetId" });
            DropIndex("dbo.Tickets", new[] { "RaceId" });
            DropIndex("dbo.TicketOrders", new[] { "TicketId" });
            DropIndex("dbo.TicketOrders", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketOrders");
            DropTable("dbo.Orders");
        }
    }
}
