namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeIdIntInCart : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserCarts");
            AlterColumn("dbo.UserCarts", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserCarts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserCarts");
            AlterColumn("dbo.UserCarts", "Id", c => c.Byte(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserCarts", "Id");
        }
    }
}
