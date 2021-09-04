namespace MatRoleClaim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Tbl_Cource_CourceCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Cources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SysllabusLink = c.String(),
                        CourceCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourceCategories", t => t.CourceCategory_Id)
                .Index(t => t.CourceCategory_Id);
            
            AddColumn("dbo.Users", "Phone", c => c.String());
            AddColumn("dbo.Users", "DateOfBirth", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "Cource_Id", c => c.Int());
            AddColumn("dbo.Users", "Cource_Id1", c => c.Int());
            CreateIndex("dbo.Users", "Cource_Id");
            CreateIndex("dbo.Users", "Cource_Id1");
            AddForeignKey("dbo.Users", "Cource_Id", "dbo.Cources", "Id");
            AddForeignKey("dbo.Users", "Cource_Id1", "dbo.Cources", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Cource_Id1", "dbo.Cources");
            DropForeignKey("dbo.Users", "Cource_Id", "dbo.Cources");
            DropForeignKey("dbo.Cources", "CourceCategory_Id", "dbo.CourceCategories");
            DropIndex("dbo.Cources", new[] { "CourceCategory_Id" });
            DropIndex("dbo.Users", new[] { "Cource_Id1" });
            DropIndex("dbo.Users", new[] { "Cource_Id" });
            DropColumn("dbo.Users", "Cource_Id1");
            DropColumn("dbo.Users", "Cource_Id");
            DropColumn("dbo.Users", "DateOfBirth");
            DropColumn("dbo.Users", "Phone");
            DropTable("dbo.Cources");
            DropTable("dbo.CourceCategories");
        }
    }
}
