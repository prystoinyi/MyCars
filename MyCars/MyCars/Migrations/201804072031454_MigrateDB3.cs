namespace MyCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCars", "Model_Id", "dbo.TypeModels");
            DropForeignKey("dbo.UserCars", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.UserCars", new[] { "Model_Id" });
            DropIndex("dbo.UserCars", new[] { "User_Id" });
            DropTable("dbo.UserCars");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserCars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserCars", "User_Id");
            CreateIndex("dbo.UserCars", "Model_Id");
            AddForeignKey("dbo.UserCars", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.UserCars", "Model_Id", "dbo.TypeModels", "Id");
        }
    }
}
