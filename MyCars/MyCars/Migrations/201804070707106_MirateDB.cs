namespace MyCars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MirateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCars", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.UserCars", new[] { "UserInfo_Id" });
            CreateTable(
                "dbo.UserInfoTypeModels",
                c => new
                    {
                        UserInfo_Id = c.Int(nullable: false),
                        TypeModel_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfo_Id, t.TypeModel_Id })
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_Id, cascadeDelete: true)
                .ForeignKey("dbo.TypeModels", t => t.TypeModel_Id, cascadeDelete: true)
                .Index(t => t.UserInfo_Id)
                .Index(t => t.TypeModel_Id);
            
            DropColumn("dbo.UserCars", "UserInfo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserCars", "UserInfo_Id", c => c.Int());
            DropForeignKey("dbo.UserInfoTypeModels", "TypeModel_Id", "dbo.TypeModels");
            DropForeignKey("dbo.UserInfoTypeModels", "UserInfo_Id", "dbo.UserInfoes");
            DropIndex("dbo.UserInfoTypeModels", new[] { "TypeModel_Id" });
            DropIndex("dbo.UserInfoTypeModels", new[] { "UserInfo_Id" });
            DropTable("dbo.UserInfoTypeModels");
            CreateIndex("dbo.UserCars", "UserInfo_Id");
            AddForeignKey("dbo.UserCars", "UserInfo_Id", "dbo.UserInfoes", "Id");
        }
    }
}
