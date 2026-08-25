namespace ToDoApp1.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty;

        // Navigation Property: Bir duruma ait birden fazla görev olabilir.
        public ICollection<ToDo> ToDos { get; set; } = new List<ToDo>();
    }
}
