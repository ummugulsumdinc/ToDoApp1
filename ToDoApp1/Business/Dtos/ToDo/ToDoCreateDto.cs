namespace ToDoApp1.Business.Dtos.ToDo
{
    public class ToDoCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int Priority { get; set; } 
        public int ProjectId { get; set; } 
        public int? StatusId { get; set; }
       
        public int UserId { get; set; }

        public int? ParentId { get; set; }// sub-todo eklerken parent id gerekecek artık

    }
}
