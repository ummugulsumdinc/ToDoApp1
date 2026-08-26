namespace ToDoApp1.Business.Dtos.ToDo
{
    public class ToDoResponseDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        
        public string? CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string? DueDate { get; set; }
        public int Priority { get; set; } // 1: Düşük, 2: Orta, 3: Yüksek

        // --- Status (Durum) Bilgileri ---
        public int StatusId { get; set; }
        public string? StatusName { get; set; }

        // --- Project (Proje) Bilgileri ---
        public int ProjectId { get; set; }
        public string? ProjectTitle { get; set; }

        // --- User (Kullanıcı) Bilgileri ---
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }

    }
}
