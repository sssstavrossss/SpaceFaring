namespace Space101.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FavoritesUserIDFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserFavorites", "ApplicationUsers_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserFavorites", new[] { "ApplicationUsers_Id" });
            DropColumn("dbo.UserFavorites", "ApplicationUserID");
            RenameColumn(table: "dbo.UserFavorites", name: "ApplicationUsers_Id", newName: "ApplicationUserID");
            AlterColumn("dbo.UserFavorites", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.UserFavorites", "ApplicationUserID", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.UserFavorites", "ApplicationUserID");
            AddForeignKey("dbo.UserFavorites", "ApplicationUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserFavorites", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.UserFavorites", new[] { "ApplicationUserID" });
            AlterColumn("dbo.UserFavorites", "ApplicationUserID", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserFavorites", "ApplicationUserID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.UserFavorites", name: "ApplicationUserID", newName: "ApplicationUsers_Id");
            AddColumn("dbo.UserFavorites", "ApplicationUserID", c => c.Int(nullable: false));
            CreateIndex("dbo.UserFavorites", "ApplicationUsers_Id");
            AddForeignKey("dbo.UserFavorites", "ApplicationUsers_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
