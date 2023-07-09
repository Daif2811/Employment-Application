namespace JobOpportunities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploadCV : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "Resume", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplyForJobs", "Resume");
        }
    }
}
