namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cartUserId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.UserCarts", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.UserCarts", name: "IX_User_Id", newName: "IX_UserId");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.UserCarts", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.UserCarts", name: "UserId", newName: "User_Id");
        }
    }
}
