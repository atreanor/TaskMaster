namespace TaskMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTaskStatusSeedDataToDB : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO TaskStatus (Name) VALUES ('To Do');");
            Sql("INSERT INTO TaskStatus (Name) VALUES ('In Progress');");
            Sql("INSERT INTO TaskStatus (Name) VALUES ('Done');");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM TaskStatus WHERE Name = 'To Do';");
            Sql("DELETE FROM TaskStatus WHERE Name = 'In Progress';");
            Sql("DELETE FROM TaskStatus WHERE Name = 'Done';");
        }
    }
}
