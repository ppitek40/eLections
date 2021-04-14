namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLandToTheUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "LandId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "LandId");
            AddForeignKey("dbo.AspNetUsers", "LandId", "dbo.Lands", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "LandId", "dbo.Lands");
            DropIndex("dbo.AspNetUsers", new[] { "LandId" });
            DropColumn("dbo.AspNetUsers", "LandId");
        }
    }
}
