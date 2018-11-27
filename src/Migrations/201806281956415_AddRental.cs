namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRental : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rentals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RentalDate = c.DateTime(nullable: false),
                        ReturnDate = c.DateTime(),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            AddColumn("dbo.Movies", "Rental_Id", c => c.Int());
            CreateIndex("dbo.Movies", "Rental_Id");
            AddForeignKey("dbo.Movies", "Rental_Id", "dbo.Rentals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Rental_Id", "dbo.Rentals");
            DropForeignKey("dbo.Rentals", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Rentals", new[] { "CustomerId" });
            DropIndex("dbo.Movies", new[] { "Rental_Id" });
            DropColumn("dbo.Movies", "Rental_Id");
            DropTable("dbo.Rentals");
        }
    }
}
