namespace GpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateposttableTypeColumn : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "Type_Id", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "Type_Id" });
            RenameColumn(table: "dbo.Posts", name: "Type_Id", newName: "TypeId");
            AlterColumn("dbo.Posts", "TypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Posts", "TypeId");
            AddForeignKey("dbo.Posts", "TypeId", "dbo.PostTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "TypeId", "dbo.PostTypes");
            DropIndex("dbo.Posts", new[] { "TypeId" });
            AlterColumn("dbo.Posts", "TypeId", c => c.Int());
            RenameColumn(table: "dbo.Posts", name: "TypeId", newName: "Type_Id");
            CreateIndex("dbo.Posts", "Type_Id");
            AddForeignKey("dbo.Posts", "Type_Id", "dbo.PostTypes", "Id");
        }
    }
}
