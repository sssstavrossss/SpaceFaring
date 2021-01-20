namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlanetDetailTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlanetDetails",
                c => new
                    {
                        PlanetID = c.Int(nullable: false),
                        RotationPeriod = c.Int(nullable: false),
                        OrbitalPeriod = c.Int(nullable: false),
                        Diameter = c.Int(nullable: false),
                        Population = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PlanetID)
                .ForeignKey("dbo.Planets", t => t.PlanetID)
                .Index(t => t.PlanetID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlanetDetails", "PlanetID", "dbo.Planets");
            DropIndex("dbo.PlanetDetails", new[] { "PlanetID" });
            DropTable("dbo.PlanetDetails");
        }
    }
}
