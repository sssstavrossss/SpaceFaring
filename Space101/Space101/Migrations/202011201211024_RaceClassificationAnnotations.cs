namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RaceClassificationAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RaceClassifications", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RaceClassifications", "Name", c => c.String());
        }
    }
}
