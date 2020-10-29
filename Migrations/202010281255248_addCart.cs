namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCart : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.UserCarts", "Cart_ID", c => c.Int());
            CreateIndex("dbo.UserCarts", "Cart_ID");
            AddForeignKey("dbo.UserCarts", "Cart_ID", "dbo.Carts", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCarts", "Cart_ID", "dbo.Carts");
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserCarts", new[] { "Cart_ID" });
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropColumn("dbo.UserCarts", "Cart_ID");
            DropTable("dbo.Carts");
        }
    }
}
