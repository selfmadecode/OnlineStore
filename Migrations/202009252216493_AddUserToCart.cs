namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToCart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCarts", "Processing", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserCarts", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserCarts", "User_Id");
            AddForeignKey("dbo.UserCarts", "User_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCarts", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserCarts", new[] { "User_Id" });
            DropColumn("dbo.UserCarts", "User_Id");
            DropColumn("dbo.UserCarts", "Processing");
        }
    }
}
