namespace JobOpportunities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleViewModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoleViewModels",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoleViewModels");
        }
    }
}
