namespace GpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterCommentTablePostId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Post_Id1", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "Post_Id1" });
            RenameColumn(table: "dbo.Comments", name: "Post_Id1", newName: "PostId");
            AlterColumn("dbo.Comments", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "PostId");
            AddForeignKey("dbo.Comments", "PostId", "dbo.Posts", "Post_Id", cascadeDelete: false);
            DropColumn("dbo.Comments", "Post_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Post_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "PostId", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostId" });
            AlterColumn("dbo.Comments", "PostId", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "PostId", newName: "Post_Id1");
            CreateIndex("dbo.Comments", "Post_Id1");
            AddForeignKey("dbo.Comments", "Post_Id1", "dbo.Posts", "Post_Id");
        }
    }
}
