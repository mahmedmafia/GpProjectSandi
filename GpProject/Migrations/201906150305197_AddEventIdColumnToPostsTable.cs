namespace GpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventIdColumnToPostsTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Event_Id", "dbo.Events");
            DropIndex("dbo.Posts", new[] { "Event_Id" });
            RenameColumn(table: "dbo.Posts", name: "Event_Id", newName: "EventId");
            AlterColumn("dbo.Posts", "EventId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "EventId");
            AddForeignKey("dbo.Posts", "EventId", "dbo.Events", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "EventId", "dbo.Events");
            DropIndex("dbo.Posts", new[] { "EventId" });
            AlterColumn("dbo.Posts", "EventId", c => c.Int());
            RenameColumn(table: "dbo.Posts", name: "EventId", newName: "Event_Id");
            CreateIndex("dbo.Posts", "Event_Id");
            AddForeignKey("dbo.Posts", "Event_Id", "dbo.Events", "Id");
        }
    }
}
