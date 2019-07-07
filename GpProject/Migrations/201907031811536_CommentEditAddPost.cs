namespace GpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentEditAddPost : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Comments", "PersonId");
            AddForeignKey("dbo.Comments", "PersonId", "dbo.People", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PersonId", "dbo.People");
            DropIndex("dbo.Comments", new[] { "PersonId" });
        }
    }
}
