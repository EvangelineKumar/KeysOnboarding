namespace KeysOnboarding.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteIsDeletedAll : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductSold", "IsDeleted");
            DropColumn("dbo.Product", "IsDeleted");
            DropColumn("dbo.Store", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Store", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ProductSold", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}
