namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addphonenumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserPhoneNumber", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "UserPhoneNumber");
        }
    }
}
