namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFlighStatusTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlightStatus",
                c => new
                    {
                        FlightStatusId = c.Byte(nullable: false),
                        StatusName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.FlightStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FlightStatus");
        }
    }
}
