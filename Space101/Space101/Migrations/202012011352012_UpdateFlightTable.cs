namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFlightTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flights", "BasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Flights", "Time");
            DropColumn("dbo.Flights", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Flights", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Flights", "Time", c => c.Int(nullable: false));
            DropColumn("dbo.Flights", "BasePrice");
        }
    }
}
