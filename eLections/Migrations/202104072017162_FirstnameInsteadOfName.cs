namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstnameInsteadOfName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "Firstname", c => c.String(nullable: false));
            DropColumn("dbo.Candidates", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Candidates", "Name", c => c.String(nullable: false));
            DropColumn("dbo.Candidates", "Firstname");
        }
    }
}
