namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EndOfElectionsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Elections", "EndOfElections", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Elections", "EndOfElections", c => c.DateTime(nullable: false));
        }
    }
}
