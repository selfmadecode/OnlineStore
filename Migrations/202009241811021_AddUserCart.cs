namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "ItemId", "dbo.Items");
            DropIndex("dbo.Carts", new[] { "ItemId" });
            CreateTable(
                "dbo.UserCarts",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        UserEmail = c.String(),
                        ItemName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        ItemId = c.Byte(nullable: false),
                        Item_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Items", t => t.Item_Id)
                .Index(t => t.Item_Id);
            
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartItemId = c.String(nullable: false, maxLength: 128),
                        UserCartId = c.String(),
                        Quantity = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        ItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartItemId);
            
            DropForeignKey("dbo.UserCarts", "Item_Id", "dbo.Items");
            DropIndex("dbo.UserCarts", new[] { "Item_Id" });
            DropTable("dbo.UserCarts");
            CreateIndex("dbo.Carts", "ItemId");
            AddForeignKey("dbo.Carts", "ItemId", "dbo.Items", "Id", cascadeDelete: true);
        }
    }
}
