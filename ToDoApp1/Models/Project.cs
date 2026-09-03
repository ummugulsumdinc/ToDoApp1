namespace ToDoApp1.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        // Foreign Key (User ile bağlantı)
        public int UserId { get; set; }

        // Navigation Property: Bir projenin içinde birden fazla görev olur.
        public ICollection<ToDo> ToDos { get; set; } = new List<ToDo>();
        public User User { get; set; } = null!;
    }
}
