namespace ToDoApp1.Business.Dtos.ToDo
{
    public class ToDoCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int Priority { get; set; } // 1: Düşük, 2: Orta, 3: Yüksek
        public int ProjectId { get; set; } // Hangi projeye ait?
        public int? StatusId { get; set; }

    }
}
