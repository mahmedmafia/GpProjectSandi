namespace GpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editdatabaseEventTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Person_Id", "dbo.People");
            DropIndex("dbo.Events", new[] { "Person_Id" });
            RenameColumn(table: "dbo.Events", name: "Person_Id", newName: "OwnerId");
            AddColumn("dbo.Events", "DayStart", c => c.DateTime());
            AddColumn("dbo.Events", "TimeStart", c => c.DateTime());
            AlterColumn("dbo.Events", "OwnerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "OwnerId");
            AddForeignKey("dbo.Events", "OwnerId", "dbo.People", "Id", cascadeDelete: true);
            DropColumn("dbo.Events", "DateStart");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "DateStart", c => c.DateTime());
            DropForeignKey("dbo.Events", "OwnerId", "dbo.People");
            DropIndex("dbo.Events", new[] { "OwnerId" });
            AlterColumn("dbo.Events", "OwnerId", c => c.Int());
            DropColumn("dbo.Events", "TimeStart");
            DropColumn("dbo.Events", "DayStart");
            RenameColumn(table: "dbo.Events", name: "OwnerId", newName: "Person_Id");
            CreateIndex("dbo.Events", "Person_Id");
            AddForeignKey("dbo.Events", "Person_Id", "dbo.People", "Id");
        }
    }
}
