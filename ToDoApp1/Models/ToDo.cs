using System.Net.NetworkInformation;

namespace ToDoApp1.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public int Priority { get; set; } // 1: Düşük, 2: Orta, 3: Yüksek

        // Foreign Key (Project ile bağlantı)
        public int ProjectId { get; set; }
        public Project Project { get; set; } = null!;

        // Foreign Key (Status ile bağlantı - isCompleted yerine)
        public int StatusId { get; set; }
        public Status Status { get; set; } = null!;
    }
}
