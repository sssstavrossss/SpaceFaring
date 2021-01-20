namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateAndPaymentIdInOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PaymentId", c => c.String());
            AddColumn("dbo.Orders", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "CreationDate");
            DropColumn("dbo.Orders", "PaymentId");
        }
    }
}
