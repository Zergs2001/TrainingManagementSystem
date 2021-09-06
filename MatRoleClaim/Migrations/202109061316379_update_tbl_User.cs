namespace MatRoleClaim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_tbl_User : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "Name");
        }
    }
}
