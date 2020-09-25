namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCategoriesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Categories (Id, Name) VALUES (1, 'Food')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (2, 'Skin Care')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (3, 'House Hold')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (4, 'Accessories')");
            Sql("INSERT INTO Categories (Id, Name) VALUES (5, 'Others')");
        }
        
        public override void Down()
        {
        }
    }
}
