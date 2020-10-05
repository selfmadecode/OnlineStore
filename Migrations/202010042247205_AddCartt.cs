namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCartt : DbMigration
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
        }

        public override void Down()
        {
        }
    }
}
