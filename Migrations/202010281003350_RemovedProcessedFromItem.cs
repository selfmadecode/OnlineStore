namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedProcessedFromItem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Items", "Processed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "Processed", c => c.Boolean(nullable: false));
        }
    }
}
