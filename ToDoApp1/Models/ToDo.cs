namespace ToDoApp1.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
