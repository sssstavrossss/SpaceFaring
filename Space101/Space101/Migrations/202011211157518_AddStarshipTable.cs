namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStarshipTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Starships",
                c => new
                    {
                        StarshipId = c.Int(nullable: false, identity: true),
                        Model = c.String(nullable: false, maxLength: 100),
                        Manufacturer = c.String(nullable: false, maxLength: 100),
                        PassengerCapacity = c.Int(nullable: false),
                        CargoCapacity = c.Int(nullable: false),
                        Length = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StarshipId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Starships");
        }
    }
}
