namespace MyCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addcascadedeleting : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TypeModels", "BrandId", "dbo.Brands");
            DropIndex("dbo.TypeModels", new[] { "BrandId" });
            AlterColumn("dbo.TypeModels", "BrandId", c => c.Int(nullable: false));
            CreateIndex("dbo.TypeModels", "BrandId");
            AddForeignKey("dbo.TypeModels", "BrandId", "dbo.Brands", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeModels", "BrandId", "dbo.Brands");
            DropIndex("dbo.TypeModels", new[] { "BrandId" });
            AlterColumn("dbo.TypeModels", "BrandId", c => c.Int());
            CreateIndex("dbo.TypeModels", "BrandId");
            AddForeignKey("dbo.TypeModels", "BrandId", "dbo.Brands", "Id");
        }
    }
}
