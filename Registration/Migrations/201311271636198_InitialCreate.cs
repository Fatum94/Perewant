namespace Registration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kompressors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PressIn = c.String(nullable: false),
                        PressOut = c.String(nullable: false),
                        Performance = c.String(nullable: false),
                        Rodo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                        isAuth = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.Kompressors");
        }
    }
}
