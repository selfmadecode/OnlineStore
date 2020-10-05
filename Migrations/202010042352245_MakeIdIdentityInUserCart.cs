namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeIdIdentityInUserCart : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserCarts");
            AlterColumn("dbo.UserCarts", "Id", c => c.Byte(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserCarts", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserCarts");
            AlterColumn("dbo.UserCarts", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.UserCarts", "Id");
        }
    }
}
