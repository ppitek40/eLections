namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedLandsToConstituencies : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Lands", newName: "Constituencies");
            RenameColumn(table: "dbo.Candidates", name: "LandId", newName: "ConstituencyId");
            RenameColumn(table: "dbo.AspNetUsers", name: "LandId", newName: "ConstituencyId");
            RenameIndex(table: "dbo.Candidates", name: "IX_LandId", newName: "IX_ConstituencyId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_LandId", newName: "IX_ConstituencyId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_ConstituencyId", newName: "IX_LandId");
            RenameIndex(table: "dbo.Candidates", name: "IX_ConstituencyId", newName: "IX_LandId");
            RenameColumn(table: "dbo.AspNetUsers", name: "ConstituencyId", newName: "LandId");
            RenameColumn(table: "dbo.Candidates", name: "ConstituencyId", newName: "LandId");
            RenameTable(name: "dbo.Constituencies", newName: "Lands");
        }
    }
}
