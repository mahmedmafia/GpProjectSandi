namespace GpProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPostTypeLookpTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PostTypes");
        }
    }
}
