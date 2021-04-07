namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedShortNameInParties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parties", "ShortName", c => c.String(nullable: false));
            AddColumn("dbo.Parties", "FullName", c => c.String());
            DropColumn("dbo.Parties", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parties", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Parties", "FullName");
            DropColumn("dbo.Parties", "ShortName");
        }
    }
}
