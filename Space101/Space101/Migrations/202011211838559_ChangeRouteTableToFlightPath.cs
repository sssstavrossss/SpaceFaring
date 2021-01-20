namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRouteTableToFlightPath : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Routes", "DepartureId", "dbo.Planets");
            DropForeignKey("dbo.Routes", "DestinationId", "dbo.Planets");
            DropForeignKey("dbo.Flights", "RouteId", "dbo.Routes");
            DropIndex("dbo.Flights", new[] { "RouteId" });
            DropIndex("dbo.Routes", new[] { "DepartureId" });
            DropIndex("dbo.Routes", new[] { "DestinationId" });
            CreateTable(
                "dbo.FlightPaths",
                c => new
                    {
                        FlightPathId = c.Int(nullable: false, identity: true),
                        DepartureId = c.Int(nullable: false),
                        DestinationId = c.Int(nullable: false),
                        Distance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.FlightPathId)
                .ForeignKey("dbo.Planets", t => t.DepartureId, cascadeDelete: true)
                .ForeignKey("dbo.Planets", t => t.DestinationId)
                .Index(t => new { t.DepartureId, t.DestinationId }, unique: true, name: "UniqueRoutes");
            
            AddColumn("dbo.Flights", "FlightPathId", c => c.Int(nullable: false));
            CreateIndex("dbo.Flights", "FlightPathId");
            AddForeignKey("dbo.Flights", "FlightPathId", "dbo.FlightPaths", "FlightPathId", cascadeDelete: true);
            DropColumn("dbo.Flights", "RouteId");
            DropTable("dbo.Routes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        RouteId = c.Int(nullable: false, identity: true),
                        DepartureId = c.Int(nullable: false),
                        DestinationId = c.Int(nullable: false),
                        Distance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.RouteId);
            
            AddColumn("dbo.Flights", "RouteId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Flights", "FlightPathId", "dbo.FlightPaths");
            DropForeignKey("dbo.FlightPaths", "DestinationId", "dbo.Planets");
            DropForeignKey("dbo.FlightPaths", "DepartureId", "dbo.Planets");
            DropIndex("dbo.Flights", new[] { "FlightPathId" });
            DropIndex("dbo.FlightPaths", "UniqueRoutes");
            DropColumn("dbo.Flights", "FlightPathId");
            DropTable("dbo.FlightPaths");
            CreateIndex("dbo.Routes", "DestinationId", unique: true);
            CreateIndex("dbo.Routes", "DepartureId", unique: true);
            CreateIndex("dbo.Flights", "RouteId");
            AddForeignKey("dbo.Flights", "RouteId", "dbo.Routes", "RouteId", cascadeDelete: true);
            AddForeignKey("dbo.Routes", "DestinationId", "dbo.Planets", "PlanetID");
            AddForeignKey("dbo.Routes", "DepartureId", "dbo.Planets", "PlanetID", cascadeDelete: true);
        }
    }
}
