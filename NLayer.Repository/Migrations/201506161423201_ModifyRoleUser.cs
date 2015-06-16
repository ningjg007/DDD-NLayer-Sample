namespace NLayer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyRoleUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("auth.Role_User", "User_Id", "auth.User");
            DropForeignKey("auth.Role_User", "Role_Id", "auth.Role");
            DropIndex("auth.Role_User", new[] { "User_Id" });
            DropIndex("auth.Role_User", new[] { "Role_Id" });
            CreateTable(
                "auth.RoleGroup_User",
                c => new
                    {
                        User_Id = c.Guid(nullable: false),
                        RoleGroup_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.RoleGroup_Id })
                .ForeignKey("auth.User", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("auth.RoleGroup", t => t.RoleGroup_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.RoleGroup_Id);
            
            AddColumn("auth.User", "Role_Id", c => c.Guid());
            CreateIndex("auth.User", "Role_Id");
            AddForeignKey("auth.User", "Role_Id", "auth.Role", "Id");
            DropTable("auth.Role_User");
        }
        
        public override void Down()
        {
            CreateTable(
                "auth.Role_User",
                c => new
                    {
                        User_Id = c.Guid(nullable: false),
                        Role_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Role_Id });
            
            DropForeignKey("auth.User", "Role_Id", "auth.Role");
            DropForeignKey("auth.RoleGroup_User", "RoleGroup_Id", "auth.RoleGroup");
            DropForeignKey("auth.RoleGroup_User", "User_Id", "auth.User");
            DropIndex("auth.RoleGroup_User", new[] { "RoleGroup_Id" });
            DropIndex("auth.RoleGroup_User", new[] { "User_Id" });
            DropIndex("auth.User", new[] { "Role_Id" });
            DropColumn("auth.User", "Role_Id");
            DropTable("auth.RoleGroup_User");
            CreateIndex("auth.Role_User", "Role_Id");
            CreateIndex("auth.Role_User", "User_Id");
            AddForeignKey("auth.Role_User", "Role_Id", "auth.Role", "Id", cascadeDelete: true);
            AddForeignKey("auth.Role_User", "User_Id", "auth.User", "Id", cascadeDelete: true);
        }
    }
}
