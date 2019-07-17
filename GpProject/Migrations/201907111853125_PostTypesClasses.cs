namespace GpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostTypesClasses : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Posts", new[] { "EventId" });
            AddColumn("dbo.Posts", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Posts", "Type_Id", c => c.Int());
            AlterColumn("dbo.Posts", "EventId", c => c.Int());
            CreateIndex("dbo.Posts", "EventId");
            CreateIndex("dbo.Posts", "Type_Id");
            AddForeignKey("dbo.Posts", "Type_Id", "dbo.PostTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "Type_Id", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "Type_Id" });
            DropIndex("dbo.Posts", new[] { "EventId" });
            AlterColumn("dbo.Posts", "EventId", c => c.Int(nullable: false));
            DropColumn("dbo.Posts", "Type_Id");
            DropColumn("dbo.Posts", "Discriminator");
            CreateIndex("dbo.Posts", "EventId");
        }
    }
}
