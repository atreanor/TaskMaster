namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DueDate = c.DateTime(),
                        StatusId = c.Int(nullable: false),
                        TaskStatus_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaskStatus", t => t.TaskStatus_Id)
                .Index(t => t.TaskStatus_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "TaskStatus_Id", "dbo.TaskStatus");
            DropIndex("dbo.Tasks", new[] { "TaskStatus_Id" });
            DropTable("dbo.Tasks");
        }
    }
}
