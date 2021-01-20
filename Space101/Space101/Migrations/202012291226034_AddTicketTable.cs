namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTicketTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TravelClasses",
                c => new
                    {
                        ClassId = c.String(nullable: false, maxLength: 10),
                        BasePriceRate = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ClassId);
            
            AddColumn("dbo.Flights", "IsVIP", c => c.Boolean(nullable: false));
            AddColumn("dbo.FlightSeats", "TravelClassId", c => c.String());
            AddColumn("dbo.FlightSeats", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.FlightSeats", "IsAvailable", c => c.Boolean(nullable: false));
            AddColumn("dbo.FlightSeats", "TravelClass_ClassId", c => c.String(maxLength: 10));
            CreateIndex("dbo.FlightSeats", "TravelClass_ClassId");
            AddForeignKey("dbo.FlightSeats", "TravelClass_ClassId", "dbo.TravelClasses", "ClassId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FlightSeats", "TravelClass_ClassId", "dbo.TravelClasses");
            DropIndex("dbo.FlightSeats", new[] { "TravelClass_ClassId" });
            DropColumn("dbo.FlightSeats", "TravelClass_ClassId");
            DropColumn("dbo.FlightSeats", "IsAvailable");
            DropColumn("dbo.FlightSeats", "Price");
            DropColumn("dbo.FlightSeats", "TravelClassId");
            DropColumn("dbo.Flights", "IsVIP");
            DropTable("dbo.TravelClasses");
        }
    }
}
