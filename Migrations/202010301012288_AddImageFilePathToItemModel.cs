namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageFilePathToItemModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "ImagePath", c => c.String(nullable: false));
            DropColumn("dbo.Items", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Image", c => c.Byte(nullable: false));
            DropColumn("dbo.Items", "ImagePath");
        }
    }
}
