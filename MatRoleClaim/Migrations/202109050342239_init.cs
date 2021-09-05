namespace MatRoleClaim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        CourceCategoryId = c.Int(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CourceCategories", t => t.CourceCategoryId)
                .ForeignKey("dbo.Users", t => t.ApplicationUser_Id)
                .Index(t => t.CourceCategoryId)
                .Index(t => t.ApplicationUser_Id);
            
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
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Phone = c.String(),
                        DateOfBirth = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            
            CreateTable(
                "dbo.RoleClaims",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        ClaimId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.ClaimId })
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Claims", t => t.ClaimId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ClaimId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TraineeInCources", "UserTrainees_Id", "dbo.Users");
            DropForeignKey("dbo.TrainerInCources", "TrainerId", "dbo.Users");
            DropForeignKey("dbo.TrainerInCources", "CourceId", "dbo.Cources");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Cources", "ApplicationUser_Id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.TraineeInCources", "CourceId", "dbo.Cources");
            DropForeignKey("dbo.Cources", "CourceCategoryId", "dbo.CourceCategories");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleClaims", "ClaimId", "dbo.Claims");
            DropForeignKey("dbo.RoleClaims", "RoleId", "dbo.Roles");
            DropIndex("dbo.RoleClaims", new[] { "ClaimId" });
            DropIndex("dbo.RoleClaims", new[] { "RoleId" });
            DropIndex("dbo.TrainerInCources", new[] { "CourceId" });
            DropIndex("dbo.TrainerInCources", new[] { "TrainerId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.TraineeInCources", new[] { "UserTrainees_Id" });
            DropIndex("dbo.TraineeInCources", new[] { "CourceId" });
            DropIndex("dbo.Cources", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Cources", new[] { "CourceCategoryId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropTable("dbo.RoleClaims");
            DropTable("dbo.TrainerInCources");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.TraineeInCources");
            DropTable("dbo.Cources");
            DropTable("dbo.CourceCategories");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Claims");
        }
    }
}
