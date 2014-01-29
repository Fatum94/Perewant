namespace Registration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usercs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Role", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Role");
        }
    }
}
