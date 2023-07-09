namespace JobOpportunities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        JobID = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false),
                        JobContent = c.String(nullable: false),
                        JobImage = c.String(nullable: false),
                        CategoryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.JobID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Jobs", new[] { "CategoryID" });
            DropTable("dbo.Jobs");
        }
    }
}
