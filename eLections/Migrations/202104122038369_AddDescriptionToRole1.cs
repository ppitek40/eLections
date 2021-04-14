namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToRole1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetRoles", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
