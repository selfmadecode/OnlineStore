namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemsTableInDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Quantity = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                        ExpiringDate = c.DateTime(),
                        Image = c.Byte(nullable: false),
                        CategoryId = c.Byte(nullable: false),
                        SupplierId = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Items", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Items", new[] { "SupplierId" });
            DropIndex("dbo.Items", new[] { "CategoryId" });
            DropTable("dbo.Suppliers");
            DropTable("dbo.Categories");
            DropTable("dbo.Items");
        }
    }
}
