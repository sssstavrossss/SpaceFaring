namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlightAndRouteTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightId = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Time = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RouteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FlightId)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: true)
                .Index(t => t.RouteId);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        DepartureId = c.Int(nullable: false),
                        DestinationId = c.Int(nullable: false),
                        Distance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RouteId)
                .ForeignKey("dbo.Planets", t => t.DepartureId, cascadeDelete: true)
                .ForeignKey("dbo.Planets", t => t.DestinationId)
                .Index(t => t.DepartureId, unique: true)
                .Index(t => t.DestinationId, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.Routes", "DestinationId", "dbo.Planets");
            DropForeignKey("dbo.Routes", "DepartureId", "dbo.Planets");
            DropIndex("dbo.Routes", new[] { "DestinationId" });
            DropIndex("dbo.Routes", new[] { "DepartureId" });
            DropIndex("dbo.Flights", new[] { "RouteId" });
            DropTable("dbo.Routes");
            DropTable("dbo.Flights");
        }
    }
}
