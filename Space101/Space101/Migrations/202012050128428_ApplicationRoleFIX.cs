namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationRoleFIX : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "Active", c => c.Boolean());
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUserRoles", "Active");
            DropColumn("dbo.AspNetUserRoles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUserRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUserRoles", "Active", c => c.Boolean());
            DropColumn("dbo.AspNetRoles", "Discriminator");
            DropColumn("dbo.AspNetRoles", "Active");
        }
    }
}
