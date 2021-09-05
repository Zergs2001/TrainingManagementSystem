namespace MatRoleClaim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Blogs", "AuthorId", "dbo.Users");
            DropIndex("dbo.Blogs", new[] { "AuthorId" });
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime());
            DropTable("dbo.Blogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        AuthorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Blogs", "AuthorId");
            AddForeignKey("dbo.Blogs", "AuthorId", "dbo.Users", "Id");
        }
    }
}
