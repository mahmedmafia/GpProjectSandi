namespace GpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditEventAddRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Events", "DayStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Events", "TimeStart", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Events", "Location", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "Location", c => c.String());
            AlterColumn("dbo.Events", "TimeStart", c => c.DateTime());
            AlterColumn("dbo.Events", "DayStart", c => c.DateTime());
            AlterColumn("dbo.Events", "Description", c => c.String());
            AlterColumn("dbo.Events", "Name", c => c.String());
        }
    }
}
