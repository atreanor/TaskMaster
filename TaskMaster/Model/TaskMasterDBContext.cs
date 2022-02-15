using System.Data.Entity;

namespace TaskMaster.Model
{
    internal class TaskMasterDBContext : DbContext
    {
        public DbSet<TaskStatus> TaskStatuses { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
