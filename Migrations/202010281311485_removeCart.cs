namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeCart : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCarts", "Cart_ID", "dbo.Carts");
            DropIndex("dbo.Carts", new[] { "UserId" });
            DropIndex("dbo.UserCarts", new[] { "Cart_ID" });
            DropColumn("dbo.UserCarts", "Cart_ID");
            DropTable("dbo.Carts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.UserCarts", "Cart_ID", c => c.Int());
            CreateIndex("dbo.UserCarts", "Cart_ID");
            CreateIndex("dbo.Carts", "UserId");
            AddForeignKey("dbo.UserCarts", "Cart_ID", "dbo.Carts", "ID");
            AddForeignKey("dbo.Carts", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
