namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomeModelChanges : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Candidates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        IsInSejm = c.Boolean(nullable: false),
                        LandId = c.Int(nullable: false),
                        PartyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Lands", t => t.LandId, cascadeDelete: true)
                .ForeignKey("dbo.Parties", t => t.PartyId, cascadeDelete: true)
                .Index(t => t.LandId)
                .Index(t => t.PartyId);
            
            CreateTable(
                "dbo.Lands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsInSejm = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Candidates", "PartyId", "dbo.Parties");
            DropForeignKey("dbo.Candidates", "LandId", "dbo.Lands");
            DropIndex("dbo.Candidates", new[] { "PartyId" });
            DropIndex("dbo.Candidates", new[] { "LandId" });
            DropTable("dbo.Parties");
            DropTable("dbo.Lands");
            DropTable("dbo.Candidates");
        }
    }
}
