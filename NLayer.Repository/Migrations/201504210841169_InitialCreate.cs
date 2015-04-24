namespace NLayer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "auth.RoleGroup",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(maxLength: 20),
                    Description = c.String(maxLength: 255),
                    SortOrder = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

            CreateTable(
                "auth.Role",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(maxLength: 20),
                    Description = c.String(maxLength: 255),
                    SortOrder = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false),
                    RoleGroup_Id = c.Guid(nullable: false)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true)
                .ForeignKey("auth.RoleGroup", t => t.RoleGroup_Id, cascadeDelete: true);

            CreateTable(
                "auth.User",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(maxLength: 20),
                    LoginName = c.String(maxLength: 80),
                    LoginPwd = c.String(maxLength: 50),
                    Email = c.String(maxLength: 50),
                    Created = c.DateTime(nullable: false)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true)
                .Index(t => t.LoginName, unique: true);

            CreateTable(
                "auth.Menu",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(maxLength: 20),
                    Code = c.String(maxLength: 255),
                    Url = c.String(maxLength: 255),
                    Module = c.String(maxLength: 50),
                    SortOrder = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Url);

            CreateTable(
                "auth.Permission",
                c => new
                {
                    Id = c.Guid(nullable: false),
                    Name = c.String(maxLength: 20),
                    Code = c.String(maxLength: 255),
                    ActionUrl = c.String(maxLength: 255),
                    SortOrder = c.Int(nullable: false),
                    Created = c.DateTime(nullable: false),
                    Menu_Id = c.Guid(nullable: false)
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.ActionUrl)
                .ForeignKey("auth.Menu", t => t.Menu_Id, cascadeDelete: true);

            CreateTable(
                "auth.Role_User",
                c => new
                {
                    User_Id = c.Guid(nullable: false),
                    Role_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new {t.User_Id, t.Role_Id})
                .ForeignKey("auth.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("auth.Role", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Role_Id);

            CreateTable(
                "auth.User_Permission",
                c => new
                {
                    User_Id = c.Guid(nullable: false),
                    Permission_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.User_Id, t.Permission_Id })
                .ForeignKey("auth.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("auth.Permission", t => t.Permission_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Permission_Id);

            CreateTable(
                "auth.Role_Permission",
                c => new
                {
                    Role_Id = c.Guid(nullable: false),
                    Permission_Id = c.Guid(nullable: false),
                })
                .PrimaryKey(t => new { t.Role_Id, t.Permission_Id })
                .ForeignKey("auth.Role", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("auth.Permission", t => t.Permission_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.Permission_Id);
        }

        public override void Down()
        {
            DropForeignKey("auth.Role", "RoleGroup_Id", "auth.RoleGroup");
            DropForeignKey("auth.Permission", "Menu_Id", "auth.Menu");
            DropForeignKey("auth.Role_User", "User_Id", "auth.User");
            DropForeignKey("auth.Role_User", "Role_Id", "auth.Role");
            DropForeignKey("auth.User_Permission", "User_Id", "auth.User");
            DropForeignKey("auth.User_Permission", "Permission_Id", "auth.Permission");
            DropForeignKey("auth.Role_Permission", "Role_Id", "auth.Role");
            DropForeignKey("auth.Role_Permission", "Permission_Id", "auth.Permission");

            DropIndex("auth.RoleGroup", new[] { "Name" });
            DropIndex("auth.Role", new[] { "Name" });
            DropIndex("auth.User", new[] { "Email" });
            DropIndex("auth.User", new[] { "LoginName" });
            DropIndex("auth.Menu", new[] { "Url" });
            DropIndex("auth.Permission", new[] { "ActionUrl" });
            DropIndex("auth.Role_User", new[] { "User_Id" });
            DropIndex("auth.Role_User", new[] { "Role_Id" });
            DropIndex("auth.User_Permission", new[] { "User_Id" });
            DropIndex("auth.User_Permission", new[] { "Permission_Id" });
            DropIndex("auth.Role_Permission", new[] { "Role_Id" });
            DropIndex("auth.Role_Permission", new[] { "Permission_Id" });

            DropTable("auth.Role");
            DropTable("auth.RoleGroup");
            DropTable("auth.User");
            DropTable("auth.Permission");
            DropTable("auth.Menu");
        }
    }
}
