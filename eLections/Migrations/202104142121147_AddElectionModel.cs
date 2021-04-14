namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddElectionModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Elections",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartOfElections = c.DateTime(nullable: false),
                        EndOfElections = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Elections");
        }
    }
}
