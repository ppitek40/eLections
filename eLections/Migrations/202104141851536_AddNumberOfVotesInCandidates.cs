namespace eLections.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberOfVotesInCandidates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidates", "NumberOfVotes", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidates", "NumberOfVotes");
        }
    }
}
