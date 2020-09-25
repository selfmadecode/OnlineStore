namespace SmartStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateSupplier : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Suppliers (Id, Name) VALUES (1, 'Roban')");
            Sql("INSERT INTO Suppliers (Id, Name) VALUES (2, 'ShopRite')");
            Sql("INSERT INTO Suppliers (Id, Name) VALUES (3, 'Spar')");
            Sql("INSERT INTO Suppliers (Id, Name) VALUES (4, 'Others')");
        }
        
        public override void Down()
        {
        }
    }
}
