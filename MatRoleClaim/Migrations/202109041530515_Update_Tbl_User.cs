namespace MatRoleClaim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Tbl_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cources", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Cources", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.Cources", "ApplicationUser_Id");
            CreateIndex("dbo.Cources", "ApplicationUser_Id1");
            AddForeignKey("dbo.Cources", "ApplicationUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Cources", "ApplicationUser_Id1", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cources", "ApplicationUser_Id1", "dbo.Users");
            DropForeignKey("dbo.Cources", "ApplicationUser_Id", "dbo.Users");
            DropIndex("dbo.Cources", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Cources", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Cources", "ApplicationUser_Id1");
            DropColumn("dbo.Cources", "ApplicationUser_Id");
        }
    }
}
