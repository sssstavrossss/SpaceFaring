namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changePaymentCollumnToInvoiceIdInOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "InvoiceId", c => c.String());
            DropColumn("dbo.Orders", "PaymentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PaymentId", c => c.String());
            DropColumn("dbo.Orders", "InvoiceId");
        }
    }
}
