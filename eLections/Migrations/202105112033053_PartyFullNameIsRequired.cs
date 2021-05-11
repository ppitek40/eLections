namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PartyFullNameIsRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Parties", "FullName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Parties", "FullName", c => c.String());
        }
    }
}
