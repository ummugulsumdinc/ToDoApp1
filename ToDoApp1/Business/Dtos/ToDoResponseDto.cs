namespace ToDoApp1.Business.Dtos
{
    public class ToDoResponseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }

        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? DueDate { get; set; }
        public int Priority { get; set; } // 1: Düşük, 2: Orta, 3: Yüksek

    }
}
