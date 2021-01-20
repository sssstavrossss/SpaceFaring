namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlanetTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Planets",
                c => new
                    {
                        PlanetID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Gravity = c.Double(nullable: false),
                        SurfaceWater = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.PlanetID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Planets");
        }
    }
}
