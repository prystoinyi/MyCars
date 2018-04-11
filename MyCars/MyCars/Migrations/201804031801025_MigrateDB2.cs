namespace MyCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserInfoes", "UserCar_Id", "dbo.UserCars");
            DropIndex("dbo.UserInfoes", new[] { "UserCar_Id" });
            AddColumn("dbo.UserCars", "UserInfo_Id", c => c.Int());
            CreateIndex("dbo.UserCars", "UserInfo_Id");
            AddForeignKey("dbo.UserCars", "UserInfo_Id", "dbo.UserInfoes", "Id");
            DropColumn("dbo.UserInfoes", "UserCar_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserInfoes", "UserCar_Id", c => c.Int());
            DropForeignKey("dbo.UserCars", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.UserCars", new[] { "UserInfo_Id" });
            DropColumn("dbo.UserCars", "UserInfo_Id");
            CreateIndex("dbo.UserInfoes", "UserCar_Id");
            AddForeignKey("dbo.UserInfoes", "UserCar_Id", "dbo.UserCars", "Id");
        }
    }
}
