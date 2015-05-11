namespace NLayer.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("auth.User", "LastLoginToken", c => c.String(maxLength: 200));
            AddColumn("auth.User", "LastLogin", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("auth.User", "LastLoginToken");
            DropColumn("auth.User", "LastLogin");
        }
    }
}
