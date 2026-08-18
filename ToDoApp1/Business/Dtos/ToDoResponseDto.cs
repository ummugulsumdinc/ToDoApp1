namespace ToDoApp1.Business.Dtos
{
    public class ToDoResponseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }

        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
    }
}
