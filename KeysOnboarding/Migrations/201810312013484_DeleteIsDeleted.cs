namespace KeysOnboarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteIsDeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Customer", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
