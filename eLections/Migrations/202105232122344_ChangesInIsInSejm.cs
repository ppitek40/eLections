namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangesInIsInSejm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "IsInParliament", c => c.Boolean(nullable: false));
            DropColumn("dbo.Candidates", "IsInSejm");
            DropColumn("dbo.Parties", "IsInSejm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Parties", "IsInSejm", c => c.Boolean(nullable: false));
            AddColumn("dbo.Candidates", "IsInSejm", c => c.Boolean(nullable: false));
            DropColumn("dbo.Candidates", "IsInParliament");
        }
    }
}
