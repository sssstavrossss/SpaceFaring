namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateFlightStatus : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO FlightStatus (FlightStatusId, StatusName) VALUES (1, 'On Scedule')");
            Sql("INSERT INTO FlightStatus (FlightStatusId, StatusName) VALUES (2, 'Delayed')");
            Sql("INSERT INTO FlightStatus (FlightStatusId, StatusName) VALUES (3, 'Canceled')");
            Sql("INSERT INTO FlightStatus (FlightStatusId, StatusName) VALUES (4, 'Departed')");
            Sql("INSERT INTO FlightStatus (FlightStatusId, StatusName) VALUES (5, 'Concluded')");
            Sql("INSERT INTO FlightStatus (FlightStatusId, StatusName) VALUES (6, 'Closed')");
        }

        public override void Down()
        {
            Sql("DELETE FROM FlightStatus WHERE FlightStatusId IN (1,2,3,4,5,6)");
        }
    }
}
