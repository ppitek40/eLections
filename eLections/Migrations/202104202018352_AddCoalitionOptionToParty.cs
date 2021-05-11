namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCoalitionOptionToParty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parties", "Coalition", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parties", "Coalition");
        }
    }
}
