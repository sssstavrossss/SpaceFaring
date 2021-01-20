namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ItemBasic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Price = c.Int(nullable: false),
                        Discount = c.Int(),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Items");
        }
    }
}
