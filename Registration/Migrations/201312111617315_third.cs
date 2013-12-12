namespace Registration.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kompressors", "Drive", c => c.String(nullable: false));
            AddColumn("dbo.Kompressors", "Power", c => c.String());
            AddColumn("dbo.Kompressors", "DegreesOfPressure", c => c.String());
            AddColumn("dbo.Kompressors", "NumberOfCylinders", c => c.String());
            AddColumn("dbo.Kompressors", "Bore", c => c.String());
            AddColumn("dbo.Kompressors", "LengthOfStroke", c => c.String());
            AddColumn("dbo.Kompressors", "SpeedOfRotation", c => c.String());
            DropColumn("dbo.Kompressors", "Rodo");
            DropColumn("dbo.Kompressors", "MyProperty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Kompressors", "MyProperty", c => c.Int(nullable: false));
            AddColumn("dbo.Kompressors", "Rodo", c => c.String(nullable: false));
            DropColumn("dbo.Kompressors", "SpeedOfRotation");
            DropColumn("dbo.Kompressors", "LengthOfStroke");
            DropColumn("dbo.Kompressors", "Bore");
            DropColumn("dbo.Kompressors", "NumberOfCylinders");
            DropColumn("dbo.Kompressors", "DegreesOfPressure");
            DropColumn("dbo.Kompressors", "Power");
            DropColumn("dbo.Kompressors", "Drive");
        }
    }
}
