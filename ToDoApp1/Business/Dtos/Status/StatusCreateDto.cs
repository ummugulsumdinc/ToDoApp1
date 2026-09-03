namespace ToDoApp1.Business.Dtos.Status
{
    public class StatusCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Colour { get; set; } = string.Empty; // Renk bilgisi (Örn: #FF0000)
        public int UserId { get; set; }
    }
}