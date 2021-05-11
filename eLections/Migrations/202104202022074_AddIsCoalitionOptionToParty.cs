namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsCoalitionOptionToParty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parties", "IsCoalition", c => c.Boolean(nullable: false));
            DropColumn("dbo.Parties", "Coalition");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parties", "Coalition", c => c.Boolean(nullable: false));
            DropColumn("dbo.Parties", "IsCoalition");
        }
    }
}
