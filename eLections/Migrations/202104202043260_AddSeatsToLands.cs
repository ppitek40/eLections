namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSeatsToLands : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lands", "Seats", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Lands", "Seats");
        }
    }
}
