namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUserRoles", "Active", c => c.Boolean());
            AddColumn("dbo.AspNetUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUserRoles", "Discriminator");
            DropColumn("dbo.AspNetUserRoles", "Active");
        }
    }
}
