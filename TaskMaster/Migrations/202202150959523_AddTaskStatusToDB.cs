namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskStatusToDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MyTasks",
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
            
            CreateTable(
                "dbo.TaskStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MyTasks", "TaskStatus_Id", "dbo.TaskStatus");
            DropIndex("dbo.MyTasks", new[] { "TaskStatus_Id" });
            DropTable("dbo.TaskStatus");
            DropTable("dbo.MyTasks");
        }
    }
}
