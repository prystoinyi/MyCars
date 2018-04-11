namespace MyCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TypeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BrandId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Brands", t => t.BrandId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.UserCars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeModels", t => t.Model_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Model_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstName = c.String(),
                        middleName = c.String(),
                        PhoneNumber = c.Int(nullable: false),
                        CarNumber = c.String(),
                        User_Id = c.String(maxLength: 128),
                        UserCar_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.UserCars", t => t.UserCar_Id)
                .Index(t => t.User_Id)
                .Index(t => t.UserCar_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "UserCar_Id", "dbo.UserCars");
            DropForeignKey("dbo.UserInfoes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCars", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserCars", "Model_Id", "dbo.TypeModels");
            DropForeignKey("dbo.TypeModels", "BrandId", "dbo.Brands");
            DropIndex("dbo.UserInfoes", new[] { "UserCar_Id" });
            DropIndex("dbo.UserInfoes", new[] { "User_Id" });
            DropIndex("dbo.UserCars", new[] { "User_Id" });
            DropIndex("dbo.UserCars", new[] { "Model_Id" });
            DropIndex("dbo.TypeModels", new[] { "BrandId" });
            DropTable("dbo.UserInfoes");
            DropTable("dbo.UserCars");
            DropTable("dbo.TypeModels");
            DropTable("dbo.Brands");
        }
    }
}
