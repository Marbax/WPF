namespace DeliveryService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedcountfromproductclass : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "Count");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Count", c => c.Int(nullable: false));
        }
    }
}
