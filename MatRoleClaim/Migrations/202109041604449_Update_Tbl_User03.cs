namespace MatRoleClaim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Tbl_User03 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Cource_Id", "dbo.Cources");
            DropForeignKey("dbo.Users", "Cource_Id1", "dbo.Cources");
            DropForeignKey("dbo.Cources", "ApplicationUser_Id1", "dbo.Users");
            DropIndex("dbo.Users", new[] { "Cource_Id" });
            DropIndex("dbo.Users", new[] { "Cource_Id1" });
            DropIndex("dbo.Cources", new[] { "ApplicationUser_Id1" });
            RenameColumn(table: "dbo.Cources", name: "CourceCategory_Id", newName: "CourceCategoryId");
            RenameIndex(table: "dbo.Cources", name: "IX_CourceCategory_Id", newName: "IX_CourceCategoryId");
            CreateTable(
                "dbo.TraineeInCources",
                c => new
                    {
                        TraineeId = c.String(nullable: false, maxLength: 128),
                        CourceId = c.Int(nullable: false),
                        UserTrainees_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.TraineeId, t.CourceId })
                .ForeignKey("dbo.Cources", t => t.CourceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserTrainees_Id)
                .Index(t => t.CourceId)
                .Index(t => t.UserTrainees_Id);
            
            CreateTable(
                "dbo.TrainerInCources",
                c => new
                    {
                        TrainerId = c.String(nullable: false, maxLength: 128),
                        CourceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TrainerId, t.CourceId })
                .ForeignKey("dbo.Cources", t => t.CourceId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.TrainerId)
                .Index(t => t.CourceId);
            
            DropColumn("dbo.Users", "Cource_Id");
            DropColumn("dbo.Users", "Cource_Id1");
            DropColumn("dbo.Cources", "ApplicationUser_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Cources", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "Cource_Id1", c => c.Int());
            AddColumn("dbo.Users", "Cource_Id", c => c.Int());
            DropForeignKey("dbo.TrainerInCources", "TrainerId", "dbo.Users");
            DropForeignKey("dbo.TrainerInCources", "CourceId", "dbo.Cources");
            DropForeignKey("dbo.TraineeInCources", "UserTrainees_Id", "dbo.Users");
            DropForeignKey("dbo.TraineeInCources", "CourceId", "dbo.Cources");
            DropIndex("dbo.TrainerInCources", new[] { "CourceId" });
            DropIndex("dbo.TrainerInCources", new[] { "TrainerId" });
            DropIndex("dbo.TraineeInCources", new[] { "UserTrainees_Id" });
            DropIndex("dbo.TraineeInCources", new[] { "CourceId" });
            DropTable("dbo.TrainerInCources");
            DropTable("dbo.TraineeInCources");
            RenameIndex(table: "dbo.Cources", name: "IX_CourceCategoryId", newName: "IX_CourceCategory_Id");
            RenameColumn(table: "dbo.Cources", name: "CourceCategoryId", newName: "CourceCategory_Id");
            CreateIndex("dbo.Cources", "ApplicationUser_Id1");
            CreateIndex("dbo.Users", "Cource_Id1");
            CreateIndex("dbo.Users", "Cource_Id");
            AddForeignKey("dbo.Cources", "ApplicationUser_Id1", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Cource_Id1", "dbo.Cources", "Id");
            AddForeignKey("dbo.Users", "Cource_Id", "dbo.Cources", "Id");
        }
    }
}
