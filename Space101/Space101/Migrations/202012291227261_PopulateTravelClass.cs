namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateTravelClass : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO TravelClasses (ClassId, BasePriceRate) VALUES ('A', 4)");
            Sql("INSERT INTO TravelClasses (ClassId, BasePriceRate) VALUES ('B', 3)");
            Sql("INSERT INTO TravelClasses (ClassId, BasePriceRate) VALUES ('C', 2)");
            Sql("INSERT INTO TravelClasses (ClassId, BasePriceRate) VALUES ('D', 1)");
            Sql("INSERT INTO TravelClasses (ClassId, BasePriceRate) VALUES ('E', 0.8)");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM TravelClasses WHERE FlightStatusId IN ('A','B','C','D','E')");
        }
    }
}
