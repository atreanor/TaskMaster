namespace TaskMaster.Model
{
    internal class TaskStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
